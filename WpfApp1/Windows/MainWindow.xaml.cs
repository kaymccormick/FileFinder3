using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Diagnostics ;
using System.Globalization ;
using System.IO ;
using System.Linq ;
using System.Net ;
using System.Reactive.Concurrency ;
using System.Reactive.Linq ;
using System.Reflection ;
using System.Threading ;
using System.Windows ;
using System.Windows.Controls ;
using System.Windows.Data ;
using System.Windows.Forms ;
using System.Windows.Input ;
using System.Windows.Markup ;
using System.Windows.Threading ;
using AppShared ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Lifetime ;
using NLog ;
using NLog.Fluent ;
using Sentinel.NLog ;
using Vanara.Extensions.Reflection ;
using WpfApp1.Application ;
using WpfApp1.Attributes ;
using WpfApp1.Logging ;
using WpfApp1.Menus ;
using WpfApp1.Xaml ;
using CheckBox = System.Windows.Controls.CheckBox ;
using Control = System.Windows.Controls.Control ;
using ILogger = NLog.ILogger ;
using Menu = System.Windows.Controls.Menu ;

namespace WpfApp1.Windows
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	/// public c
	[ WindowMetadata ( "Main Window" ) ]
	public partial class MainWindow : Window , IHaveLogger , IHaveAppLogger

	{

		public static DependencyProperty
			LifetimeScopeProperty = AppShared.App.LifetimeScopeProperty ;

		public static DependencyProperty MenuItemListCollectionViewProperty = AppShared.App.MenuItemListCollectionViewProperty ;
		/// <summary>Adds a specified object as the child of a <see cref="T:System.Windows.Controls.ContentControl" />. </summary>
		/// <param name="value">The object to add.</param>
		protected override void AddChild ( object value )
		{
			Logger.Debug ( $"{nameof ( AddChild )}:{value} {value.GetType ( )}" ) ;
			base.AddChild ( value ) ;
		}

		public void RecurseDiscover ( object ui )
		{
			if ( ui == null )
			{
				Logger.Warn ( $"null" ) ;
				return ;
			}

			if ( ui.GetType ( ) == typeof ( string ) )
			{
				//	Logger.Warn ( $"string is {(ui as string).Substring(0, 32)}" ) ;
				return ;
			}

			var uie = ui as UIElement ;
			var fe = ui as FrameworkElement ;
			string desc = null ;
			var qqq = Attribute.GetCustomAttribute (
			                                        ui.GetType ( )
			                                      , typeof ( RuntimeNamePropertyAttribute )
			                                       ) ;
			if ( qqq != null )
			{
				desc = ui.GetPropertyValue < string > (
				                                       ( qqq as RuntimeNamePropertyAttribute ).Name
				                                      ) ;
			}
			else
			{
				if ( fe     == null
				     && uie == null )
				{
					return ;
				}

				desc = fe != null ? fe.Name : uie.Uid ;
			}

			var qq = Attribute.GetCustomAttribute (
			                                       ui.GetType ( )
			                                     , typeof ( ContentPropertyAttribute )
			                                      ) ;
			Logger.Error ( $"child {desc} ({ui.GetType ( )})" ) ;
			if ( qq != null )
			{
				var content =
					ui.GetPropertyValue < object > ( ( qq as ContentPropertyAttribute ).Name ) ;
				if ( content is IEnumerable
				     && content.GetType ( ) != typeof ( string ) )
				{
					foreach ( var child in content as IEnumerable )
					{
						RecurseDiscover ( child ) ;
					}
				}
				else
				{
					RecurseDiscover ( content ) ;
				}

				return ;
			}


			var c = ui as Control ;

			if ( ui is ItemsControl ic )
			{
				foreach ( var item in ic.Items )
				{
					RecurseDiscover ( item ) ;
				}
			}
			else if ( ui is ContentControl cc )
			{
				RecurseDiscover ( cc.Content ) ;
			}
		}

		public MainWindow ( )
		{
			InitializeComponent ( ) ;

			AddHandler ( AppShared.App.LifetimeScopeChangedEvent, new RoutedPropertyChangedEventHandler < ILifetimeScope > ( UpdatedScope ));
			Loaded += OnLoaded;

			//RecurseDiscover(Content ) ;
			var target = MyCacheTarget.GetInstance ( 1000 ) ;
			target.Cache.SubscribeOn ( Scheduler.Default )
			      .Buffer ( TimeSpan.FromMilliseconds ( 100 ) )
			      .Where ( x => x.Any ( ) )
			      .ObserveOnDispatcher ( DispatcherPriority.Background )
			      .Subscribe (
			                  infos => {
				                  foreach ( var info in infos )
				                  {
					                  if ( info.Level != LogLevel.Trace )
					                  {
						                  LogEvents.Add ( info ) ;
					                  }
				                  }
			                  }
			                 ) ; /*
			DataTemplate lvItemTemplate = (DataTemplate) FindResource("ButtonTemplate");
			target.Cache.SubscribeOn(Scheduler.Default).Buffer(TimeSpan.FromMilliseconds(100)).Where(x => x.Any())
				.ObserveOnDispatcher(DispatcherPriority.Background).Subscribe(infos =>
				{
					foreach (LogEventInfo info in infos)
					{
						ListView lv = new ListView();
						lv.ItemTemplate = lvItemTemplate;
						lv.Items.Add(info);
						InlineUIContainer container = new InlineUIContainer(lv);
						var paragraph = new Paragraph(new Run(info.FormattedMessage));
						paragraph.Inlines.Add(container);
						FlowDoc.Blocks.Add(paragraph);

					}
				});
				*/

			AddHandler (
			            AppShared.App.MenuItemListCollectionViewChangedEvent
			          , new RoutedPropertyChangedEventHandler < CollectionView > (
			                                                                       OnMenuItemListCollectionViewChanged
			                                                                      )
			           ) ;
			//Vanara.PInvoke.User32.SetWindowLong( Vanara.PInvoke.User32.GetActiveWindow(), User32.WindowLongFlags.GWL_EXSTYLE )
		}

		private void UpdatedScope (
			object                                           sender
		  , RoutedPropertyChangedEventArgs < ILifetimeScope > e
		)
		{
			Logger.Warn ( "updated scope " + e.NewValue.Tag ) ;
		}

		private void OnLoaded ( object sender , RoutedEventArgs e )
		{
			DoRestart                                       = false ;
			System.Windows.Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose ;
		}

		public LogEventInfoCollection LogEvents { get ; set ; } = new LogEventInfoCollection (
		                                                                                      new[]
		                                                                                      {
			                                                                                      new
				                                                                                      LogEventInfo (
				                                                                                                    LogLevel
					                                                                                                   .Info
				                                                                                                  , "derp"
				                                                                                                  , "lala 123"
				                                                                                                   )
			                                                                                    , new
				                                                                                      LogEventInfo (
				                                                                                                    LogLevel
					                                                                                                   .Info
				                                                                                                  , "derp"
				                                                                                                  , "lala 123"
				                                                                                                   )
			                                                                                    , new
				                                                                                      LogEventInfo (
				                                                                                                    LogLevel
					                                                                                                   .Debug
				                                                                                                  , "deep"
				                                                                                                  , "lala 123"
				                                                                                                   )
		                                                                                      }
		                                                                                     ) ;

		public object _LogEventInfos { get ; set ; }

		public AppLogger AppLogger { get ; set ; }

		public ILogger Logger { get ; set ; } = LogManager.GetCurrentClassLogger ( ) ;

		private void OnMenuItemListCollectionViewChanged (
			object                                             sender
		  , RoutedPropertyChangedEventArgs < CollectionView > e
		)
		{
			if ( e.Source == this )
			{
				var menu = Template.FindName ( "appMenu" , this ) as Menu ;
				var c = e.NewValue ;

				foreach ( IMenuItem menuItem in c )
				{
					menu.Items.Add ( MenuHelper.MakeMenuItem ( menuItem ) ) ;
				}

				Logger.Warn ( e.RoutedEvent.Name + " changed" ) ;
				DumpRoutedPropertyChangedEventArgs ( e ) ;
				e.Handled = true ;
			}
			else
			{
				Logger.Warn ( $"ignoring {e.RoutedEvent.Name} from {e.Source}" ) ;
			}
		}

		private void DumpRoutedPropertyChangedEventArgs < T > (
			RoutedPropertyChangedEventArgs < T > args
		)
		{
			Logger.Debug ( "OldValue = "    + args.OldValue ) ;
			Logger.Debug ( "NewValue = "    + args.NewValue ) ;
			Logger.Debug ( "RoutedEvent = " + args.RoutedEvent ) ;
			Logger.Debug ( "Source = "      + args.Source ) ;
		}

		private void Refresh_OnClick ( object sender , RoutedEventArgs e ) { }

		private void CommandBinding_OnPreviewExecuted ( object sender , ExecutedRoutedEventArgs e )
		{
			Logger.Debug ( "Preview can execute" ) ;
		}

		public ILifetimeScope LifetimeScope
		{
			get => ( ILifetimeScope ) GetValue ( LifetimeScopeProperty ) ;
			set => SetValue ( LifetimeScopeProperty , value ) ;
		}

		private void CommandBinding_OnExecuted ( object sender , ExecutedRoutedEventArgs e )
		{
			var module1BinDebugModule1Dll = @"..\..\..\..\Module1\module1\bin\debug\module1.dll" ;

			var f = new FileInfo ( module1BinDebugModule1Dll ) ;
			if ( ! f.Exists )
			{
				Logger.Warn ( "dll does not exist" ) ;
				return ;
			}

			try
			{
				Logger.Warn ( $"Loading assembly {f.FullName}" ) ;
				var loadFile = Assembly.LoadFile ( f.FullName ) ;
				var childScope = LifetimeScope.BeginLifetimeScope (
				                                                   b => {
					                                                   b.RegisterAssemblyModules (
					                                                                              loadFile
					                                                                             ) ;
				                                                   }
				                                                  ) ; //LifetimeScope = childScope ;
			}
			catch ( Exception ex )
			{
				Logger.Fatal ( ex , $"{ex.Message}" ) ;
			}

			//Vanara.Windows.Forms.;
		}

		private void DumpDebug ( object sender , ExecutedRoutedEventArgs e )
		{
			var foo = LifetimeScope.Resolve < IEnumerable < TraceListener > > ( ) ;
			foreach ( var q in foo )
			{
			}
		}

		private void OnRestart ( object sender , ExecutedRoutedEventArgs e )
		{
			DoRestart                                       = true ;
			System.Windows.Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown ;
			Close ( ) ;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Window.Closed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnClosed ( EventArgs e )
		{
			if ( DoRestart )
			{
				var newWindow = new MainWindow ( ) ;
				newWindow.Show ( ) ;
			}

			base.OnClosed ( e ) ;
		}

		public bool DoRestart { get ; set ; }

		private void Restart ( object sender , ExecutedRoutedEventArgs e )
		{
			DoRestart = true ;
			System.Windows.Application.Current.ShutdownMode =ShutdownMode.OnExplicitShutdown; 
			Close ( ) ;
		}

		private void InstancesOnly_OnChecked ( object sender , RoutedEventArgs e )
		{
			Logger.Debug ( "checked" ) ;
			CheckBox x = sender as CheckBox ;
			var collectionViewSource = x.TryFindResource("Registrations") as CollectionViewSource ;
			var tryFindResource = TryFindResource ( "RegistrationConverter" ) ;
			if ( tryFindResource == null )
			{
				return ;
			}

			var converter = tryFindResource as IValueConverter ;
			if ( converter == null )
			{
				return ;
			}

			CheckedHandler = ( object o , FilterEventArgs args ) => {
				args.Accepted = false ;
				var componentRegistration = args.Item as IComponentRegistration ;
				var convert = converter.Convert (
				                                 args.Item
				                               , typeof ( int )
				                               , "Count"
				                               , CultureInfo.CurrentUICulture
				                                ) ;
				try
				{
					var count = ( int ) convert ;
					if ( count > 0 )
					{
						args.Accepted = true ;
					}
				}
				catch ( Exception  )
				{
					return ;
				}
			} ;
			collectionViewSource.Filter += CheckedHandler ;
		}

		public FilterEventHandler CheckedHandler { get ; set ; }


		private void InstancesOnly_OnUnchecked ( object sender , RoutedEventArgs e )
		{
			CheckBox x = sender as CheckBox ;
			var collectionViewSource = x.TryFindResource("Registrations") as CollectionViewSource ;
			collectionViewSource.Filter -= CheckedHandler ;
			CheckedHandler              =  null ;
		}

		private void ButtonBase_OnClick ( object sender , RoutedEventArgs e )
		{
			{
				var provider = new NLogViewerProvider (
				                                       new NetworkSettings ( )
				                                       {
					                                       Port = NetConfig.Port
					                                     , Protocol =
						                                       NetConfig.IsUdp
							                                       ? NetworkProtocol.Udp
							                                       : NetworkProtocol.Tcp
					                                      ,
				                                       }
				                                      ) ;
				provider.Start ( ) ;
				provider.Logger = MyLogger ;
			}
		}

		public Sentinel.Interfaces.ILogger MyLogger { get ; set ; } = new MyLogger ( ) ;

		private void LoadInstance ( object sender , ExecutedRoutedEventArgs e )
		{
			var v = e.Parameter.GetPropertyValue<object> ( "Value" ) ;
			//Lazy<object> l = e.Parameter as Lazy < object > ;
			//var v = l.Value ;
			Logger.Debug ( "loaded " + v ) ;
		}
	}
}