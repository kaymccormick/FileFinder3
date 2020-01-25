using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Diagnostics ;
using System.IO ;
using System.Linq ;
using System.Text ;
using System.Windows ;
using System.Windows.Data ;
using System.Windows.Input ;
using System.Windows.Threading ;
using System.Xaml ;
using System.Xml.Serialization ;
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

		/// <summary>Raises the <see cref="E:System.Windows.Application.Startup" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
		protected override void OnStartup ( StartupEventArgs e )
		{
			PresentationTraceSources.Refresh();
			var nLogTraceListener = new NLogTraceListener ( ) ;
			var routedEventSource = PresentationTraceSources.RoutedEventSource ;
			nLogTraceListener.DefaultLogLevel = LogLevel.Debug;
			nLogTraceListener.ForceLogLevel = LogLevel.Warn ;
			nLogTraceListener.LogFactory = AppContainer.Resolve < LogFactory > ( ) ;
			nLogTraceListener.AutoLoggerName = false ;
			//nLogTraceListener.
			routedEventSource.Switch.Level = SourceLevels.All ;
			routedEventSource.Listeners.Add(new AppTraceLisener());
			routedEventSource.Listeners.Add ( nLogTraceListener ) ;
			base.OnStartup ( e ) ;
		}
	}

	public class AppTraceLisener : TraceListener
	{
		private static Logger Logger = NLog.LogManager.GetCurrentClassLogger ( ) ;
		private NLogTextWriter _nLogTextWriter ;
		public AppTraceLisener ( ) { _nLogTextWriter = new NLogTextWriter ( Logger ) ; }

		/// <summary>Writes trace and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		public override void TraceEvent (
			TraceEventCache eventCache
		  , string          source
		  , TraceEventType  eventType
		  , int             id
		)
		{
			int i = 0 ;
			foreach ( var q in eventCache.LogicalOperationStack )
			{
				Logger.Error ( $"{id}{i}: {source} {q.GetType ( )}: {q}" ) ;
				i ++ ;
			}
		}

		/// <summary>Writes trace information, a message, and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		public override void TraceEvent (
			TraceEventCache eventCache
		  , string          source
		  , TraceEventType  eventType
		  , int             id
		  , string          message
		)
		{
			base.TraceEvent (
			                 eventCache
			               , source
			               , eventType
			               , id
			               , message
			                ) ;
		}

		/// <summary>Writes trace information, a formatted array of objects and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		public override void TraceEvent (
			TraceEventCache eventCache
		  , string          source
		  , TraceEventType  eventType
		  , int             id
		  , string          format
		  , params object[] args
		)
		{
			SerializableDictionary <string, object> d = new SerializableDictionary < string , object > (); 
			Dictionary <string, string> xmlDict = new Dictionary < string , string > ();
			bool doOutput = true ;
			for ( int i = 0 ; i < args.Length - 1 ; i += 2 )
			{
				string key = args[ i ] as string ;
				var o = args[ i + 1 ] ;
				string desc = null ;
				if(o is RoutedEvent re)
				{
					if(re.Name == "ScrollChanged")
					{
						doOutput = false ;
					}
					desc = re.Name ;
				} else
				if(o is FrameworkElement fe)
				{
					desc = $"{o.GetType ( ).Name}[{fe.Name}]" ;
				} else if ( o is bool )
				{
					desc = o.GetType ( ) + "[" + o + "]" ;
				} else if(o is RoutedEventArgs a)
				{
					desc = o.GetType ( ).ToString ( ) ;
				}
			
				//d[ args[ i ].ToString ( ) ] = args[ i + 1 ] ;
				if(desc != null)
				{
					xmlDict[ key ] = desc ;
					continue ;
				}
				try
				{
					// if ( args[ i + 1 ] is RoutedEvent xxxx )
					// {
					var xamlXmlWriterSettings = new XamlXmlWriterSettings ( ) ;
					StringBuilder b = new StringBuilder ( ) ;
					//XamlWriter persist = new XamlXmlWriter(_nLogTextWriter);
					System.Windows.Markup.XamlWriter.Save (
					                                       args[ i + 1 ]
					                                     , new StringWriter ( b )
					                                      ) ;
					xmlDict[ args[ i ].ToString ( ) ] = b.ToString ( ) ;
				}
				catch ( Exception )
				{
					if(desc == null) throw ;
					xmlDict[ key ] = desc ;

					try
					{
						XmlSerializer serializer = new XmlSerializer ( args[ i + 1 ].GetType ( ) ) ;
						StringBuilder b = new StringBuilder ( ) ;
						serializer.Serialize ( new StringWriter ( b ) , args[ i + 1 ] ) ;
						xmlDict[ args[ i ].ToString ( ) ] = b.ToString ( ) ;
					}
					catch(Exception)
					{

					}
				} 
			}

			if ( ! doOutput ) return ;
			Logger.Trace (
			             String.Join (
			                          "; "
			                        , xmlDict.AsQueryable ( )
			                                 .Select (
			                                          ( pair , i ) => $"{pair.Key} = {pair.Value}"
			                                         )
			                         )
			            ) ;

		}

		/// <summary>When overridden in a derived class, writes the specified message to the listener you create in the derived class.</summary>
		/// <param name="message">A message to write.</param>
		public override void Write ( string message )
		{
			Logger.Debug(message);
			
		}

		/// <summary>When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.</summary>
		/// <param name="message">A message to write.</param>
		public override void WriteLine ( string message )
		{
			Logger.Debug ( message ) ;
		}
	}

	public class NLogTextWriter : TextWriter
	{
		/// <summary>Writes a string followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The string to write. If <paramref name="value" /> is <see langword="null" />, only the line terminator is written.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		public override void WriteLine ( string value ) { Logger.Warn ( value ) ; }

		public Logger Logger { get ; }

		public NLogTextWriter ( Logger logger ) { Logger = logger ; }

		/// <summary>When overridden in a derived class, returns the character encoding in which the output is written.</summary>
		/// <returns>The character encoding in which the output is written.</returns>
		public override Encoding Encoding { get ; } = Encoding.UTF8;
	}

	public interface IHaveAppLogger
	{
		AppLogger AppLogger { get ; set ; }
	}
}