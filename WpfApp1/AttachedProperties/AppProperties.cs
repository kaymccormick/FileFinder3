using System.ComponentModel ;
using System.Windows ;
using System.Windows.Data ;
using Autofac ;
using NLog ;


namespace WpfApp1.AttachedProperties
{
	public static class AppProperties
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;


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
			                                                                    FrameworkPropertyMetadataOptions.Inherits
			                                                                   ,
			                                                                    OnMenuItemListCollectionViewChanged
			                                                                  , CoerceMenuItemListCollectionView
			                                                                  , false
			                                                                  , UpdateSourceTrigger
				                                                                   .PropertyChanged
			                                                                   )
			                                    ) ;


		// ReSharper disable once MemberCanBePrivate.Global
		public static readonly RoutedEvent LoggerRegisteredEvent =
			EventManager.RegisterRoutedEvent (
			                                  "LoggerRegistered"
			                                , RoutingStrategy.Direct
			                                , typeof ( RoutedEventHandler )
			                                , typeof ( AppProperties )
			                                 ) ;

		private static object CoerceMenuItemListCollectionView (
			DependencyObject d
		  , object           baseValue
		)
		{
			return baseValue ;
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
#if TRACE_EVENTS
				Logger.Trace ( $"Raising event on UIElement {evt.Name}" ) ;
#endif
				uie.RaiseEvent ( ev ) ;
			}
			else if ( d is ContentElement ce )
			{
#if TRACE_EVENTS
				Logger.Trace ( $"Raising event on ContentElement {evt.Name}" ) ;
#endif
				ce.RaiseEvent ( ev ) ;
			}
			else
			{
#if TRACE_EVENTS
				Logger.Trace ( $"Raising event on incompatible type {evt.Name}" ) ;
#endif
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
#if TRACE_EVENTS
			Logger.Trace ( $"{nameof ( SetMenuItemListCollectionView )} {target}, {value}" ) ;
#endif

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

		// ReSharper disable once MemberCanBePrivate.Global
		public static readonly RoutedEvent LifetimeScopeChangedEvent =
			EventManager.RegisterRoutedEvent (
			                                  "LifetimeScopeChanged"
			                                , RoutingStrategy.Direct
			                                , typeof ( RoutedPropertyChangedEventHandler <
				                                  ILifetimeScope > )
			                                , typeof ( AppProperties )
			                                 ) ;

		public static readonly DependencyProperty LifetimeScopeProperty =
			DependencyProperty.RegisterAttached (
			                                     "LifetimeScope"
			                                   , typeof ( ILifetimeScope )
			                                   , typeof ( AppProperties )
			                                   , new FrameworkPropertyMetadata (
			                                                                    null
			                                                                  , FrameworkPropertyMetadataOptions
				                                                                   .Inherits
			                                                                  , OnLifetimeScopeChanged
			                                                                  , CoerceLifetimeScopeValue
			                                                                  , false
			                                                                  , UpdateSourceTrigger
				                                                                   .PropertyChanged
			                                                                   )
			                                    ) ;

		private static object CoerceLifetimeScopeValue ( DependencyObject d , object basevalue )
		{
			return basevalue ;

		}


		private static void OnLifetimeScopeChanged (
			DependencyObject                   d
		  , DependencyPropertyChangedEventArgs e
		)
		{
			var evt = LifetimeScopeChangedEvent ;
			var ev = new RoutedPropertyChangedEventArgs < ILifetimeScope > (
			                                                                ( ILifetimeScope ) e
				                                                               .OldValue
			                                                              , ( ILifetimeScope ) e
				                                                               .NewValue
			                                                              , evt
			                                                               ) ;
			if ( d is UIElement uie )
			{
#if TRACE_EVENTS
				Logger.Trace ( $"Raising event on UIElement {evt.Name}" ) ;
#endif
				uie.RaiseEvent ( ev ) ;
			}
			else if ( d is ContentElement ce )
			{
#if TRACE_EVENTS
				Logger.Trace ( $"Raising event on ContentElement {evt.Name}" ) ;
#endif
				ce.RaiseEvent ( ev ) ;
			}
			else
			{
#if TRACE_EVENTS
				Logger.Trace ( $"Raising event on incompatible type {evt.Name}" ) ;
#endif
			}
		}

		public static void AddLifetimeScopeChangedEventHandler (
			DependencyObject   d
		  , RoutedEventHandler handler
		)
		{
			if ( d is UIElement uie )
			{
				uie.AddHandler ( LifetimeScopeChangedEvent , handler ) ;
			}
			else if ( d is ContentElement ce )
			{
				ce.AddHandler ( LifetimeScopeChangedEvent , handler ) ;
			}
		}

		public static void RemoveLifetimeScopeChangedEventHandler (
			DependencyObject   d
		  , RoutedEventHandler handler
		)
		{
			if ( d is UIElement uie )
			{
				uie.AddHandler ( LifetimeScopeChangedEvent , handler ) ;
			}
			else if ( d is ContentElement ce )
			{
				ce.AddHandler ( LifetimeScopeChangedEvent , handler ) ;
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
		public static ILifetimeScope GetLifetimeScope ( DependencyObject target )
		{
			return ( ILifetimeScope ) target.GetValue ( LifetimeScopeProperty ) ;
		}

		public static void SetLifetimeScope ( DependencyObject target , ILifetimeScope value )
		{
#if TRACE_EVENTS
			Logger.Trace ( $"{nameof ( SetMenuItemListCollectionView )} {target}, {value}" ) ;
#endif

			target.SetValue ( LifetimeScopeProperty , value ) ;
		}
	}
}