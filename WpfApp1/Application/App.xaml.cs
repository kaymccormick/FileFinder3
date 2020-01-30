using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Diagnostics ;
using System.Linq ;
using System.Reflection ;
using System.Runtime.ExceptionServices ;
using System.Runtime.Serialization ;
using System.Threading ;
using System.Threading.Tasks ;
using System.Windows ;
using System.Windows.Data ;
using System.Windows.Input ;
using System.Windows.Threading ;
using AppShared ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Autofac.Extras.DynamicProxy ;
using Common ;
using Common.Converters ;
using Common.Logging ;
using Common.Utils ;
using DynamicData.Annotations ;
using Microsoft.Scripting.Utils ;
using NLog ;
using Vanara.Extensions ;
using Vanara.Extensions.Reflection ;
using WpfApp1.DataSource ;
using WpfApp1.Logging ;
using WpfApp1.Util ;
using WpfApp1.Windows ;
using WpfApp1.Xaml ;
using IContainer = Autofac.IContainer ;
using LogLevel = NLog.LogLevel ;
using LogManager = NLog.LogManager ;
using RegistrationConverter = Common.Converters.RegistrationConverter ;

namespace WpfApp1.Application
{
	public enum ExitCode { Success = 0 , GeneralError = 1 , ArgumentsError = 2 , }

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
			DoLogMethod = new LoggerProxyHelper.LogMethod ( doLogMessage ) ;
			try
			{
				var s = new TaskCompletionSource < int > ( ) ;

				TCS = s ;
				var cd = AppDomain.CurrentDomain ;
				cd.AssemblyLoad += CurrentDomainOnAssemblyLoad ;
				//cd.TypeResolve += CdOnTypeResolve;
				cd.ProcessExit += ( sender , args ) => {
					DoLogMethod ( $"Exiting. args is {args}" ) ;
				} ;
				cd.UnhandledException += CdOnUnhandledException ;
				cd.ResourceResolve    += CdOnResourceResolve ;

				cd.FirstChanceException += CurrentDomainOnFirstChanceException ;
			}
			catch ( Exception ex )
			{
				DoLogMethod ( ex.ToString ( ) + "exception in constructor" ) ;
			}
		}

		public LoggerProxyHelper.LogMethod DoLogMethod { get ; set ; }

		private void doLogMessage (
			string message
		  , string callerfilepath
		  , string callermembername
		)
		{
			Logger?.Debug ( message ) ;
			Debug.WriteLine ( message ) ;
		}


		public TaskCompletionSource < int > TCS { get ; set ; }

		private Assembly CdOnResourceResolve ( object sender , ResolveEventArgs args )
		{
			DoLogMethod ( $"nameof(CdOnResourceResolve): {args.Name}" ) ;
			return null ;
		}

		private void CdOnUnhandledException ( object sender , UnhandledExceptionEventArgs e )
		{
			var message = "" ;
			message = e.ExceptionObject.GetPropertyValue < string > ( "Message" ) ;
			var err = new UnhandledException (
			                                  "UnhandledException: " + message
			                                , e.ExceptionObject as Exception
			                                 ) ;

			var formattableString = $"{err.Message} Terminating={e.IsTerminating}" ;
			DoLogMethod ( formattableString ) ;
			// if ( Logger == null )
			// {
			// LogManager.GetCurrentClassLogger ( L
			// .Error ( err , formattableString ) ;

			// }
			// else
			// {

			// Logger?.Error ( err , formattableString ) ;
			// }
		}


		private Assembly CdOnTypeResolve ( object sender , ResolveEventArgs args )
		{
			DoLogMethod ( $"{args.Name}" ) ;
			DoLogMethod ( $"Requesting assembly is {args.RequestingAssembly.FullName}" ) ;
			return null ;
		}

		private void CurrentDomainOnAssemblyLoad ( object sender , AssemblyLoadEventArgs args )
		{
			if ( Logger != null )
			{
				Logger.Trace ( args.LoadedAssembly ) ;
			}
			else
			{
				//Debug.WriteLine ( args.LoadedAssembly ) ;
			}
		}

		private void CurrentDomainOnFirstChanceException (
			object                        sender
		  , FirstChanceExceptionEventArgs e
		)
		{
			try
			{
				var msg = $"{e.Exception.Message}" ;
				DoLogMethod ( msg ) ;
				Debug.WriteLine ( "Exception: " + e.Exception ) ;
				var inner = e.Exception.InnerException ;
				var seen = new HashSet < object > ( ) ;
				while ( inner != null
				        && ! seen.Contains ( inner ) )
				{
					DoLogMethod ( inner.Message ) ;
					inner = inner.InnerException ;
				}
			}
			catch ( Exception ex )
			{
				Debug.WriteLine ( "Exception: " + ex ) ;
			}
		}

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
			DoLogMethod ( $"{sender} {e.Parameter}" ) ;
		}

		[ System.Diagnostics.CodeAnalysis.SuppressMessage (
			                                                  "Usage"
			                                                , "VSTHRD001:Avoid legacy thread switching APIs"
			                                                , Justification = "<Pending>"
		                                                  ) ]
		private object DispatcherOperationCallback ( object arg )
		{
			DoLogMethod ( $"{nameof ( DispatcherOperationCallback )}" ) ;

			AppInitialize ( ) ;

			MainWindow mainWindow = null ;
			try
			{
				mainWindow = AppContainer.Resolve < MainWindow > ( ) ;
				DoLogMethod ( $"Reeeived {mainWindow} " ) ;
			}
			catch ( Exception ex )
			{
				DoLogMethod ( "Cant resolve newwindow: " + ex.Message ) ;
				mainWindow = new MainWindow ( ) ;
			}

			if ( ShowMainWindow )
			{
				try
				{
					//mainWindow.WindowState = WindowState.Minimized ;
					mainWindow.Show ( ) ;
				}
				catch ( Exception ex )
				{
					DoLogMethod ( ex.Message ) ; //?.Error ( ex , ex.Message ) ;
				}
#if SHOWWINDOW
				var mainWindow = new MainWindow();
				mainWindow.Show();
#endif
			}

			
			Initialized = true ;

			return null ;
		}

		public void AppInitialize ( )
		{
			AppContainer = ContainerHelper.SetupContainer ( out var container ) ;
			_container   = container ;

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

			var objectIdProvider = AppContainer.Resolve < IObjectIdProvider > ( ) ;

			var converter = new RegistrationConverter ( AppContainer , objectIdProvider ) ;
			Resources[ "RegistrationConverter" ] = converter ;
			Stage2Complete                       = true ;
			TCS.SetResult ( 1 ) ;
		}

		public bool Stage2Complete { get ; set ; }

		public bool Initialized { get ; set ; }

		public bool ShowMainWindow { get ; set ; } = true ;

		public bool DoTracing { get ; } = false ;


		private void MainWindowLoaded ( object o , RoutedEventArgs args )
		{
			var w = o as MainWindow ;
			if ( w == null ) { }

			{
				Logger.Error ( $"Bad type for event sender {o.GetType ( )}" ) ;
			}

			var fe = o as FrameworkElement ;
			DoLogMethod ( $"{nameof ( MainWindowLoaded )}" ) ;
			AppShared.App.SetMenuItemListCollectionView ( fe , MenuItemListCollectionView ) ;
			DoLogMethod ( $"Setting LifetimeScooe DependencyProperty to {AppContainer}" ) ;
			AppShared.App.SetAssemblyList (
			                               w
			                             , new AssemblyList (
			                                                 AppDomain
				                                                .CurrentDomain.GetAssemblies ( )
			                                                )
			                              ) ;
			AppShared.App.SetContainer ( fe , _container ) ;
			AppShared.App.SetLifetimeScope ( fe , AppContainer ) ;
			DoLogMethod ( $"{w.LifetimeScope} - {w.LifetimeScope.Tag}" ) ;
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
				DoLogMethod ( ex.Message ) ;
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
			var msg =
				$"{nameof ( Application_DispatcherUnhandledException )}: {e.Exception.Message}" ;
			DoLogMethod ( msg ) ;
			var inner = e.Exception.InnerException ;
			var seen = new HashSet < object > ( ) ;
			while ( inner != null
			        && ! seen.Contains ( inner ) )
			{
				DoLogMethod ( inner.Message ) ;
				inner = inner.InnerException ;
			}
		}

		private void App_OnExit ( object sender , ExitEventArgs e )
		{
			DoLogMethod ( $"Appliccation exiting.  Exit code is {e.ApplicationExitCode}" ) ;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Application.Startup" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
		protected override void OnStartup ( StartupEventArgs e )
		{
			DoOnStartup ( e.Args) ;
			
			base.OnStartup ( e ) ;
		}

		public void DoOnStartup ( string[] args )
		{
			AddEventListeners ( ) ;
			if ( ProcessArgs && args.Any ( ) )
			{
				var windowName = args[ 0 ] ;
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
				var dispatcherOperation = Dispatcher.BeginInvoke (
				                                                  DispatcherPriority.Send
				                                                , ( DispatcherOperationCallback )
				                                                  DispatcherOperationCallback
				                                                , null
				                                                 ) ;
				DispatcherOp = dispatcherOperation ;
			}
		}
		public DispatcherOperation DispatcherOp { get ; set ; }

		public bool ProcessArgs { get ; set ; } = false ;

		private void Application_DispatcherUnhandledException1 (
			object                                sender
		  , DispatcherUnhandledExceptionEventArgs e
		)
		{
			Logger.Error ( e.Exception , $"Unhandled exception {e.Exception}" ) ;
			var inner = e.Exception.InnerException ;
			var seen = new HashSet < object > ( ) ;
			while ( inner != null
			        && ! seen.Contains ( inner ) )
			{
				Logger.Debug ( inner , inner.Message ) ;
				inner = inner.InnerException ;
			}

			ErrorExit ( ExitCode.GeneralError ) ;
			// foreach (var window in Windows)
			// {
			//     ((Window) window).Close();
			// }
		}

		private void ErrorExit ( ExitCode exitcode = ExitCode.GeneralError )
		{
			if ( exitcode != null )
			{
				var code = Convert.ChangeType ( exitcode , exitcode.GetTypeCode ( ) ) ;
				if ( code != null )
				{
					var intCode = ( int ) code ;

					DoLogMethod ( $"Exiting with code {intCode}, {exitcode}" ) ;
					if ( Current == null )
					{
						DoLogMethod ( "No application reference" ) ;
						Process.GetCurrentProcess ( ).Kill ( ) ;
					}
					else
					{
						Current.Shutdown ( intCode ) ;
					}
				}
			}
		}
	}

	public class UnhandledException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class.</summary>
		public UnhandledException ( ) { }

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		public UnhandledException ( string message ) : base ( message ) { }

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		public UnhandledException ( string message , Exception innerException ) : base (
		                                                                                message
		                                                                              , innerException
		                                                                               )
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is <see langword="null" /> or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
		protected UnhandledException (
			[ NotNull ] SerializationInfo info
		  , StreamingContext              context
		) : base ( info , context )
		{
		}
	}
}