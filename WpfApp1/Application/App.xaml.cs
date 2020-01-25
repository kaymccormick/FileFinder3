using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Diagnostics ;
using System.Linq ;
using System.Windows ;
using System.Windows.Data ;
using System.Windows.Input ;
using System.Windows.Threading ;
using Autofac ;
using Autofac.Core ;
using Autofac.Extras.DynamicProxy ;
using Microsoft.Scripting.Utils ;
using NLog ;
using Vanara.Extensions ;
using WpfApp1.AttachedProperties ;
using WpfApp1.Commands ;
using WpfApp1.DataSource ;
using WpfApp1.Interfaces ;
using WpfApp1.Logging ;
using WpfApp1.Menus ;
using WpfApp1.Util ;
using WpfApp1.Windows ;
using IContainer = Autofac.IContainer ;

namespace WpfApp1.Application
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App : System.Windows.Application , IHaveAppLogger
	{
		public AppLogger AppLogger { get ; set ; } = null ;

		public ILogger Logger { get ; set ; }

		public App ( )
		{
			AppContainer = ContainerHelper.SetupContainer ( ) ;

			var loggerTracker = AppContainer.Resolve < ILoggerTracker > ( ) ;
			var myLoggerName = typeof ( App ).FullName ;
			loggerTracker.LoggerRegistered += ( sender , args ) => {
				if ( args.Logger.Name == myLoggerName )
				{
					args.Logger.Trace (
					                   "Received logger for application in LoggerREegistered handler."
					                  ) ;
				}

				if ( Logger == null )
				{
					Debug.WriteLine ( "got a logger but i dont have one yet" ) ;
				}
			} ;

			Logger = AppContainer.Resolve < ILogger > (
			                                           new TypedParameter (
			                                                               typeof ( Type )
			                                                             , typeof ( App )
			                                                              )
			                                          ) ;
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
			Logger.Debug ( "Application logger initialized." ) ;
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


		public ILifetimeScope AppContainer { get ; set ; }

		public MenuItemList MyMenuItemList { get ; private set ; }

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
			AddEventListeners ( ) ;
			if ( e.Args.Any ( ) )
			{
				var windowName = e.Args[ 0 ] ;
				var xaml = "../Windows/" + windowName+ ".xaml" ;
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
		}

		private object DispatcherOperationCallback ( object arg )
		{
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
			var mainWindow = AppContainer.Resolve < MainWindow > ( ) ;
			Logger.Trace ( $"Reeeived {mainWindow} " ) ;
			mainWindow.Show ( ) ;
#if SHOWWINDOW
                var mainWindow = new MainWindow();
                mainWindow.Show();
#endif
				return null ;
		}

		private void MainWindowLoaded ( object o , RoutedEventArgs args )
		{
			if ( ! typeof ( MainWindow ).IsAssignableFrom ( o.GetType ( ) ) )
			{
				Logger.Error ( $"Bad type for event sender {o.GetType ( )}" ) ;
			}

			var fe = o as FrameworkElement ;
			Logger.Info ( $"{nameof ( MainWindowLoaded )}" ) ;
			AppProperties.SetMenuItemListCollectionView ( fe , MenuItemListCollectionView ) ;
			Logger.Debug ( $"Setting LifetimeScooe DependencyProperty" ) ;
			AppProperties.SetLifetimeScope ( fe , AppContainer ) ;
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
			if ( Logger != null )
			{
				Logger.Fatal (
				              e.Exception
				            , $"{nameof ( Application_DispatcherUnhandledException )}: {e.Exception.Message}"
				             ) ;
			}
		}

		private void App_OnExit ( object sender , ExitEventArgs e )
		{
			if ( Logger != null )
			{
				Logger.Warn ($"Appliccation exiting.  Exit code is {e.ApplicationExitCode}" )  ;
			}
		}
	}

	public interface IHaveAppLogger
	{
		AppLogger AppLogger { get ; set ; }
	}
}