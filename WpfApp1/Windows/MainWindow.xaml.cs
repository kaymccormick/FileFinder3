﻿using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.Collections.ObjectModel ;
using System.ComponentModel ;
using System.Linq ;
using System.Reactive.Concurrency ;
using System.Reactive.Linq ;
using System.Threading ;
using System.Windows ;
using System.Windows.Controls ;
using System.Windows.Input ;
using System.Windows.Markup ;
using System.Windows.Threading ;
using DynamicData.Annotations ;
using NLog ;
using NLog.Fluent ;
using Vanara.Extensions.Reflection ;
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
		/// <summary>Adds a specified object as the child of a <see cref="T:System.Windows.Controls.ContentControl" />. </summary>
		/// <param name="value">The object to add.</param>
		protected override void AddChild ( object value )
		{
			Logger.Debug ( $"{nameof ( AddChild )}:{value} {value.GetType ( )}" ) ;
			base.AddChild ( value ) ;
		}

		public void RecurseDiscover ( object ui )
		{
			if(ui == null)
			{
				Logger.Warn ( $"null" ) ;
				return ;
			}

			if(ui.GetType() == typeof(string))
			{
			//	Logger.Warn ( $"string is {(ui as string).Substring(0, 32)}" ) ;
				return ;
			}
			UIElement uie = ui as UIElement;
			FrameworkElement fe = ui as FrameworkElement	;
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
			Logger.Error( $"child {desc} ({ui.GetType()})" ) ;
			if(qq != null)
			{
				var content = ui.GetPropertyValue<object> ( ( qq as ContentPropertyAttribute ).Name ) ;
				if ( content is IEnumerable && content.GetType (  ) != typeof(string))
				{
					foreach ( var child in content as IEnumerable)
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

			
			Control c = ui as Control ;

			if ( ui is ItemsControl ic )
			{
				foreach(var item in ic.Items)
				{
					RecurseDiscover(item);
				}
			} else if ( ui is ContentControl cc )
			{
				RecurseDiscover	(cc.Content);
			}
		}
		public MainWindow ( )
		{
			InitializeComponent ( ) ;

			RecurseDiscover(Content ) ;
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

		public ILogger Logger { get ; set ; } = LogManager.GetCurrentClassLogger ( ) ;

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

		private void CommandBinding_OnExecuted ( object sender , ExecutedRoutedEventArgs e )
		{
			//Vanara.Windows.Forms.;
		}
	}
}
