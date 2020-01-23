using System.ComponentModel ;
using System.Windows ;
using System.Windows.Data ;
using NLog ;


namespace WpfApp1.AttachedProperties
{
	public static class AppProperties
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger (         ) ;


		public static readonly RoutedEvent MenuItemListCollectionViewChangedEvent =
			EventManager.RegisterRoutedEvent (
			                                  "MenuItemListCollectionViewChanged"
			                                , RoutingStrategy.Direct
			                                , typeof ( RoutedPropertyChangedEventHandler <
				                                  ICollectionView > )
			                                , typeof ( AppProperties )
			                                 ) ;

		public static readonly DependencyProperty MenuItemListCollectionViewProperty =
			DependencyProperty.RegisterAttached (
			                                     "MenuItemListCollectionView"
			                                   , typeof ( ICollectionView )
			                                   , typeof ( AppProperties )
			                                   , new FrameworkPropertyMetadata (
			                                                                    null
			                                                                   ,
			                                                                    // FrameworkPropertyMetadataOptions.None,
			                                                                    FrameworkPropertyMetadataOptions
				                                                                   .Inherits
			                                                                   ,
			                                                                    // | FrameworkPropertyMetadataOptions
			                                                                    // .OverridesInheritanceBehavior,
			                                                                    OnMenuItemListCollectionViewChanged
			                                                                  , CoerceMenuItemListCollectionView
			                                                                  , false
			                                                                  , UpdateSourceTrigger
				                                                                   .PropertyChanged
			                                                                   )
			                                    ) ;



		public static readonly RoutedEvent LoggerRegisteredEvent =
			EventManager.RegisterRoutedEvent (
			                                  "LoggerRegistered"
			                                , RoutingStrategy.Direct
			                                , typeof ( RoutedEventHandler )
			                                , typeof ( AppProperties )
			                                 ) ;

		private static object CoerceMenuItemListCollectionView (
			DependencyObject d
		  , object           basevalue
		)
		{
			return basevalue ;
		}

		private static void OnMenuItemListCollectionViewChanged (
			DependencyObject                   d
		  , DependencyPropertyChangedEventArgs e
		)
		{
			var evt = MenuItemListCollectionViewChangedEvent ;
			var ev = new RoutedPropertyChangedEventArgs < ICollectionView > (
			                                                                 ( ICollectionView ) e
				                                                                .OldValue
			                                                               , ( ICollectionView ) e
				                                                                .NewValue
			                                                               , evt
			                                                                ) ;
			if ( d is UIElement uie )
			{
				Logger.Trace ( $"Raising event on UIElement {evt.Name}" ) ;
				uie.RaiseEvent ( ev ) ;
			}
			else if ( d is ContentElement ce )
			{
				Logger.Trace ( $"Raising event on ContentElement {evt.Name}" ) ;
				ce.RaiseEvent ( ev ) ;
			}
			else
			{
				Logger.Trace ( $"Raising event on incompatible type {evt.Name}" ) ;
			}
		}

		public static void AddOnMenuItemListCollectionViewChangedHandler (
			DependencyObject   d
		  , RoutedEventHandler handler
		)
		{
			if ( d is UIElement uie )
			{
				uie.AddHandler ( MenuItemListCollectionViewChangedEvent , handler ) ;
			}
			else if ( d is ContentElement ce )
			{
				ce.AddHandler ( MenuItemListCollectionViewChangedEvent , handler ) ;
			}
		}

		public static void RemoveOnMenuItemListCollectionViewChangedHandler (
			DependencyObject   d
		  , RoutedEventHandler handler
		)
		{
			if ( d is UIElement uie )
			{
				uie.AddHandler ( MenuItemListCollectionViewChangedEvent , handler ) ;
			}
			else if ( d is ContentElement ce )
			{
				ce.AddHandler ( MenuItemListCollectionViewChangedEvent , handler ) ;
			}
		}

		// [AttachedPropertyBrowsableForType(typeof(Window))]
		/// <summary>
		///     Helper for getting <see cref="MenuItemListCollectionViewProperty" />
		///     from <paramref name="target" />.
		/// </summary>
		/// <param name="target">
		///     <see cref="DependencyObject" /> to read
		///     <see cref="MenuItemListCollectionViewProperty" /> from.
		/// </param>
		/// <returns>MenuItemListCollectionView property value.</returns>
		[ AttachedPropertyBrowsableForType ( typeof ( Window ) ) ]
		[ AttachedPropertyBrowsableForType ( typeof ( FrameworkElement ) ) ]
		// [AttachedProperty    BrowsableForType(typeof(ItemsControl))]
		public static ICollectionView GetMenuItemListCollectionView ( DependencyObject target )
		{
			Logger.Trace ( $"{nameof ( GetMenuItemListCollectionView )} {target}" ) ;
			return ( CollectionView ) target.GetValue ( MenuItemListCollectionViewProperty ) ;
		}

		public static void SetMenuItemListCollectionView (
			DependencyObject target
		  , ICollectionView  value
		)
		{
			Logger.Trace ( $"{nameof ( SetMenuItemListCollectionView )} {target}, {value}" ) ;

			target.SetValue ( MenuItemListCollectionViewProperty , value ) ;
		}


		public static void AddLoggerRegisteredHHandler (
			DependencyObject   d
		  , RoutedEventHandler handler
		)
		{
			if ( d is UIElement uie )
			{
				uie.AddHandler ( LoggerRegisteredEvent , handler ) ;
			}
			else
			{
				var fe = d as FrameworkElement ;
				fe?.AddHandler ( LoggerRegisteredEvent , handler ) ;
			}
		}

		public static void RemoveLoggerRegisteredHHandler (
			DependencyObject   d
		  , RoutedEventHandler handler
		)
		{
			if ( d is UIElement uie )
			{
				uie.RemoveHandler ( LoggerRegisteredEvent , handler ) ;
			}
			else
			{
				var fe = d as FrameworkElement ;
				fe?.RemoveHandler ( LoggerRegisteredEvent , handler ) ;
			}
		}

	}

}
