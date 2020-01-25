using System ;
using System.Collections.Generic ;
using System.Collections.ObjectModel ;
using System.ComponentModel ;
using System.Linq ;
using System.Reactive.Concurrency ;
using System.Reactive.Linq ;
using System.Windows ;
using System.Windows.Controls ;
using System.Windows.Input ;
using System.Windows.Threading ;
using DynamicData.Annotations ;
using NLog ;
using WpfApp1.Application ;
using WpfApp1.AttachedProperties ;
using WpfApp1.Attributes ;
using WpfApp1.Interfaces ;
using WpfApp1.Logging ;
using WpfApp1.Menus ;

namespace WpfApp1.Windows
{
	public class LogEventInfoCollection : ObservableCollection < LogEventInfo >
	{
		/// <summary>
		///     Initializes a new instance of the
		///     <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />
		///     class.
		/// </summary>
		public LogEventInfoCollection (  ) { }

		/// <summary>
		///     Initializes a new instance of the
		///     <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />
		///     class that contains elements copied from the specified list.
		/// </summary>
		/// <param name="list">The list from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///     The <paramref name="list" />
		///     parameter cannot be <see langword="null" />.
		/// </exception>
		public LogEventInfoCollection ( [ NotNull ] List < LogEventInfo > list ) : base ( list ) { }

		/// <summary>
		///     Initializes a new instance of the
		///     <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />
		///     class that contains elements copied from the specified collection.
		/// </summary>
		/// <param name="collection">The collection from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///     The
		///     <paramref name="collection" /> parameter cannot be <see langword="null" />.
		/// </exception>
		public LogEventInfoCollection ( [ NotNull ] IEnumerable < LogEventInfo > collection ) :
			base ( collection )
		{
		}
	}

	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	/// public c
	[ WindowMetadata ( "Main Window" ) ]
	public partial class MainWindow : Window , IHaveLogger , IHaveAppLogger
	{
		public MainWindow ( )
		{
			InitializeComponent ( ) ;

			
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
			            AppProperties.MenuItemListCollectionViewChangedEvent
			          , new RoutedPropertyChangedEventHandler < ICollectionView > (
			                                                                       OnMenuItemListCollectionViewChanged
			                                                                      )
			           ) ;
			//Vanara.PInvoke.User32.SetWindowLong( Vanara.PInvoke.User32.GetActiveWindow(), User32.WindowLongFlags.GWL_EXSTYLE )
		}

		public LogEventInfoCollection LogEvents { get ; set ; } = new LogEventInfoCollection (
		                                                                                      new []
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

		public ILogger Logger { get ; set ; }

		private void OnMenuItemListCollectionViewChanged (
			object                                             sender
		  , RoutedPropertyChangedEventArgs < ICollectionView > e
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
			Logger.Debug ( "OldValue = " + args.OldValue ) ;
			Logger.Debug ( "NewValue = " + args.NewValue ) ;
			Logger.Debug ( "RoutedEvent = " + args.RoutedEvent ) ;
			Logger.Debug ( "Source = " + args.Source ) ;
		}

		private void Refresh_OnClick ( object sender , RoutedEventArgs e ) { }

		private void CommandBinding_OnPreviewExecuted ( object sender , ExecutedRoutedEventArgs e )
		{
			Logger.Debug ( "Preview can execute" ) ;
		}
	}
}
