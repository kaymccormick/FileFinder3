using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Diagnostics ;
using System.Linq ;
using System.Reflection ;
using System.Runtime.ExceptionServices ;
using System.Threading ;
using System.Windows ;
using System.Windows.Data ;
using System.Windows.Input ;
using System.Windows.Threading ;
using AppShared ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Autofac.Extras.DynamicProxy ;
using Microsoft.Scripting.Utils ;
using NLog ;
using Vanara.Extensions ;
using WpfApp1.Commands ;
using WpfApp1.DataSource ;
using WpfApp1.Logging ;
using WpfApp1.Util ;
using WpfApp1.Windows ;
using WpfApp1.Xaml ;
using IContainer = Autofac.IContainer ;

namespace WpfApp1.Application
{
	public enum ExitCode
	{
		Success        = 0,
		GeneralError   = 1,
		ArgumentsError = 2,
	}

	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App : System.Windows.Application , IHaveAppLogger
	{
		private IContainer _container ;

		public AppLogger AppLogger { get ; set ; } = null ;

		public ILogger Logger { get ; set ; }

		public App ( )
		{
			var cd = AppDomain.CurrentDomain ;
			cd.AssemblyLoad += CurrentDomainOnAssemblyLoad;
			//cd.TypeResolve += CdOnTypeResolve;
			cd.ProcessExit += ( sender , args ) => {
				Logger?.Warn ( args == null ? "null" : args.ToString ( ) ) ;
			};
			cd.UnhandledException += CdOnUnhandledException;
			cd.ResourceResolve += CdOnResourceResolve;
			
			cd.FirstChanceException += CurrentDomainOnFirstChanceException;
#if DEBUGB_AUTOFAC_REGS
			Logger.Debug (
						  "reg: "
						  + string.Join (
										 ", "
						   , AppContainer
										.ComponentRegistry.Registrations.Select ( RegOutput )
										.ToList ( )
										)
						 ) ;
#endif
//			Logger.Debug ( "Application logger initialized." ) ;
		}

		private Assembly CdOnResourceResolve ( object sender , ResolveEventArgs args )
		{
			Logger.Warn ( $"{args.Name}" );
			return null ;
		}

		private void CdOnUnhandledException ( object sender , UnhandledExceptionEventArgs e )
		{
			Logger.Error ( $"{e.ExceptionObject} {e.IsTerminating}" ) ;
		}



		private Assembly CdOnTypeResolve ( object sender , ResolveEventArgs args )
		{
			Logger.Warn ( $"{args.Name}" );
			Logger.Warn($"Requesting assembly is {args.RequestingAssembly.FullName}");
			return null ;
		}

		private void CurrentDomainOnAssemblyLoad ( object sender , AssemblyLoadEventArgs args )
		{
			if ( Logger != null )
			{
				Logger.Warn ( $"{args.LoadedAssembly}" ) ;
			}
			else
			{
				Debug.WriteLine ( args.LoadedAssembly ) ;
			}
		}

		private void CurrentDomainOnFirstChanceException (
			object                        sender
		  , FirstChanceExceptionEventArgs e
		)
		{
			try
			{
				Logger?.Trace ( $"{e.Exception.Message}" ) ;
			} catch(Exception ex)
			{
				Debug.WriteLine ( ( ex.Message ) ) ;
			}

	;	}
			
		private string RegOutput ( IComponentRegistration registration , int i )
		{
			var registrationActivator = registration.Activator ;
			if ( registrationActivator != null )
			{
				var registrationActivatorLimitType = registrationActivator.LimitType ;
				if ( registrationActivatorLimitType != null )
				{
					return registrationActivatorLimitType.FullName ;
				}
			}

			return "" ;
		}


		private ILifetimeScope AppContainer { get ; set ; }

		private void OpenWindowExecuted ( object sender , ExecutedRoutedEventArgs e )
		{
			Logger.Info ( $"{sender} {e.Parameter}" ) ;
		}

		[ System.Diagnostics.CodeAnalysis.SuppressMessage (
															  "Usage"
															, "VSTHRD001:Avoid legacy thread switching APIs"
															, Justification = "<Pending>"
														  ) ]
		private void ApplicationStartup ( object sender , StartupEventArgs e )
		{
		}

		private object DispatcherOperationCallback ( object arg )
		{
			AppContainer = ContainerHelper.SetupContainer (out var container ) ;
			_container = container ;

			
			PresentationTraceSources.Refresh ( ) ;
			if ( DoTracing )
			{
				var nLogTraceListener = new NLogTraceListener ( ) ;
				var routedEventSource = PresentationTraceSources.RoutedEventSource ;
				nLogTraceListener.DefaultLogLevel = LogLevel.Debug ;
				nLogTraceListener.ForceLogLevel   = LogLevel.Warn ;
				//nLogTraceListener.LogFactory      = AppContainer.Resolve < LogFactory > ( ) ;
				nLogTraceListener.AutoLoggerName = false ;
				//nLogTraceListener.
				routedEventSource.Switch.Level = SourceLevels.All ;
				var foo = AppContainer.Resolve < IEnumerable < TraceListener > > ( ) ;
				foreach ( var tl in foo )
				{
					routedEventSource.Listeners.Add ( tl ) ;
				}

				//routedEventSource.Listeners.Add ( new AppTraceLisener ( ) ) ;
				routedEventSource.Listeners.Add ( nLogTraceListener ) ;
			}


			var loggerTracker = AppContainer.Resolve < ILoggerTracker > ( ) ;
			var myLoggerName = typeof ( App ).FullName ;
			loggerTracker.LoggerRegistered += ( sender , args ) => {
				if ( args.Logger.Name == myLoggerName )
				{
					args.Logger.Trace (
									   "Received logger for application in LoggerREegistered handler."
									  ) ;
				}
				else
				{
					if ( Logger == null )
					{
						Debug.WriteLine ( "got a logger but i dont have one yet" ) ;
					}
				}
			} ;

			Logger = AppContainer.Resolve < ILogger > (
													   new TypedParameter (
																		   typeof ( Type )
																		 , typeof ( App )
																		  )
													  ) ;


			var menuItemList = AppContainer.Resolve < IMenuItemList > ( ) ;
			MenuItemListCollectionView = new ListCollectionView ( menuItemList ) ;
			var handler = new RoutedEventHandler ( MainWindowLoaded ) ;

			EventManager.RegisterClassHandler (
											   typeof ( Window )
											 , FrameworkElement.LoadedEvent
											 , handler
											  ) ;
			Resources[ "MyMenuItemList" ] = menuItemList ;
			Logger.Trace ( $"Attempting to resolve MainWindow" ) ;

			var objectIdProvider = AppContainer.Resolve < IObjectIdProvider > ( ) ;
			RegistrationConverter converter = new RegistrationConverter ( AppContainer, objectIdProvider ) ;
			Resources[ "RegistrationConverter" ] = converter ;
			var mainWindow = AppContainer.Resolve < MainWindow > ( ) ;
			Logger.Trace ( $"Reeeived {mainWindow} " ) ;

			try
			{
				mainWindow.Show ( ) ;
			} catch(Exception ex)
			{
				Logger?.Error ( ex , ex.Message ) ;

			}
#if SHOWWINDOW
				var mainWindow = new MainWindow();
				mainWindow.Show();
#endif
			return null ;
		}

		public bool DoTracing { get ; } = false ;


		private void MainWindowLoaded ( object o , RoutedEventArgs args )
		{
			MainWindow w = o as MainWindow;
			if (w == null) {}
			{
				Logger.Error ( $"Bad type for event sender {o.GetType ( )}" ) ;
			}

			var fe = o as FrameworkElement ;
			Logger.Info ( $"{nameof ( MainWindowLoaded )}" ) ;
			AppShared.App.SetMenuItemListCollectionView ( fe , MenuItemListCollectionView ) ;
			Logger.Debug ( $"Setting LifetimeScooe DependencyProperty to {AppContainer}" ) ;
			AppShared.App.SetAssemblyList(w, new AssemblyList(AppDomain.CurrentDomain.GetAssemblies()));
			AppShared.App.SetContainer(fe, _container);
			AppShared.App.SetLifetimeScope ( fe , AppContainer ) ;
			Logger.Info ( $"{w.LifetimeScope} - {w.LifetimeScope.Tag}" ) ;
		}

		// ReSharper disable once MemberCanBePrivate.Global
		public ListCollectionView MenuItemListCollectionView { get ; set ; }

		private void AddEventListeners ( )
		{
			try
			{
				EventManager.RegisterClassHandler (
												   typeof ( Window )
												 , UIElement.KeyDownEvent
												 , new KeyEventHandler ( OnKeyDown )
												  ) ;
			}
			catch ( Exception ex )
			{
				Logger.Error ( ex , ex.Message ) ;
			}
		}

		private void OnKeyDown ( object sender , KeyEventArgs e )
		{
			if ( e.Key                         == Key.T
				 && e.KeyboardDevice.Modifiers == ( ModifierKeys.Control | ModifierKeys.Alt ) )
			{
				Process.Start (
							   new ProcessStartInfo (
													 @".\Demo.XamlDesigner.exe"
												   , @"..\WpfApp1\Windows\MainWindow.xaml"
													) { WorkingDirectory = @"..\..\..\tools" }
							  ) ;
			}
		}

		private void Application_DispatcherUnhandledException (
			object                                sender
		  , DispatcherUnhandledExceptionEventArgs e
		)
		{
			Logger?.Fatal (
						   e.Exception
						 , $"{nameof ( Application_DispatcherUnhandledException )}: {e.Exception.Message}"
						  ) ;
		}

		private void App_OnExit ( object sender , ExitEventArgs e )
		{
			Logger?.Warn ( $"Appliccation exiting.  Exit code is {e.ApplicationExitCode}" ) ;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Application.Startup" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
		protected override void OnStartup ( StartupEventArgs e )
		{
			AddEventListeners ( ) ;
			if ( e.Args.Any ( ) )
			{
				var windowName = e.Args[ 0 ] ;
				var xaml = "../Windows/" + windowName + ".xaml" ;
				var converter = TypeDescriptor.GetConverter ( typeof ( Uri ) ) ;
				if ( converter.CanConvertFrom ( typeof ( string ) ) )
				{
					StartupUri = ( Uri ) converter.ConvertFrom ( xaml ) ;
					Logger.Debug ( "Startup URI is {startupUri}" , StartupUri ) ;
				}
			}
			else
			{
				Dispatcher.BeginInvoke (
										DispatcherPriority.Send
									  , ( DispatcherOperationCallback ) DispatcherOperationCallback
									  , null
									   ) ;
			}
			base.OnStartup ( e ) ;
		}
		private void Application_DispatcherUnhandledException1(object                                                         sender,
															   System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Logger.Error(e.Exception, $"Unhandled exception {e.Exception}");
			Exception inner = e.Exception.InnerException;
			HashSet<object> seen = new HashSet<object>();
			while (inner != null && !seen.Contains(inner))
			{
				Logger.Debug(inner, inner.Message);
				inner = inner.InnerException;
			}

			ErrorExit(ExitCode.GeneralError);
			// foreach (var window in Windows)
			// {
			//     ((Window) window).Close();
			// }
		}

		private void ErrorExit(ExitCode exitcode = ExitCode.GeneralError)
		{
			if (exitcode != null)
			{
				object code = Convert.ChangeType(exitcode, exitcode.GetTypeCode());
				if (code != null)
				{
					int intCode = (int) code;

					Logger.Info($"Exiting with code {intCode}, {exitcode}");
					if (System.Windows.Application.Current == null)
					{
						Logger.Debug("No application reference");
						System.Diagnostics.Process.GetCurrentProcess().Kill();
					}
					else
					{
						System.Windows.Application.Current.Shutdown(intCode);
					}
				}
			}
		}

	}
}