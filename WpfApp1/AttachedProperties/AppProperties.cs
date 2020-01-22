using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using NLog;

namespace WpfApp1.AttachedProperties
{
    public static class AppProperties
    {

        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();


        public static readonly RoutedEvent MenuItemListCollectionViewChangedEvent = EventManager.RegisterRoutedEvent("MenuItemListCollectionViewChanged",
                                                                                                                     RoutingStrategy.Direct,
                                                                                                                     typeof(RoutedPropertyChangedEventHandler <ICollectionView>),
            typeof(AppProperties));

        public static readonly DependencyProperty MenuItemListCollectionViewProperty =
	        DependencyProperty.RegisterAttached( "MenuItemListCollectionView",
	                                             typeof(ICollectionView), typeof(AppProperties),
	                                             new FrameworkPropertyMetadata( null,
                                                                                // FrameworkPropertyMetadataOptions.None,
	                                                                            FrameworkPropertyMetadataOptions.Inherits,
	                                                                            // | FrameworkPropertyMetadataOptions
		                                                                           // .OverridesInheritanceBehavior,
                                                                               new  PropertyChangedCallback(OnMenuItemListCollectionViewChanged),
                                                                               new CoerceValueCallback(CoerceMenuItemListCollectionView),
                                                                               false,
                                                                               UpdateSourceTrigger.PropertyChanged
	                                                                          ) );

        private static object CoerceMenuItemListCollectionView(
	        DependencyObject d,
	        object           basevalue
        )
        {

	        return basevalue;
        }

        private static void OnMenuItemListCollectionViewChanged(
	        DependencyObject                   d,
	        DependencyPropertyChangedEventArgs e
        )
        {
	        var evt = AppProperties.MenuItemListCollectionViewChangedEvent;
	        RoutedPropertyChangedEventArgs < ICollectionView > ev =
		        new RoutedPropertyChangedEventArgs < ICollectionView >( (ICollectionView)e.OldValue, 
		                                                                (ICollectionView)e.NewValue,
		                                                              evt );
            if(d is UIElement uie)  
            {
	            Logger.Debug($"Raising event on UIElement {evt.Name}");
                uie.RaiseEvent(ev);
            } else if ( d is ContentElement ce )
            {
	            Logger.Debug($"Raising event on ContentElement {evt.Name}");
                ce.RaiseEvent(ev);
            }
            else
            {
	            Logger.Debug($"Raising event on incompatible type {evt.Name}");
            }
        }

        public static void AddOnMenuItemListCollectionViewChangedHandler(
	        DependencyObject   d,
	        RoutedEventHandler handler
        )
        {
	        if (d is UIElement uie)
	        {
		        uie.AddHandler(AppProperties.MenuItemListCollectionViewChangedEvent, handler);
	        }
	        else if (d is ContentElement ce)
	        {
		        ce.AddHandler(AppProperties.MenuItemListCollectionViewChangedEvent, handler);
	        }
        }

        public static void RemoveOnMenuItemListCollectionViewChangedHandler(
	        DependencyObject   d,
	        RoutedEventHandler handler
        )
        {
	        if (d is UIElement uie)
	        {
		        uie.RemoveHandler(AppProperties.MenuItemListCollectionViewChangedEvent, handler);
	        }
	        else if (d is ContentElement ce)
	        {
		        ce.RemoveHandler(AppProperties.MenuItemListCollectionViewChangedEvent, handler);
	        }
        }

        // [AttachedPropertyBrowsableForType(typeof(Window))]
        /// <summary>Helper for getting <see cref="MenuItemListCollectionViewProperty"/> from <paramref name="target"/>.</summary>
        /// <param name="target"><see cref="DependencyObject"/> to read <see cref="MenuItemListCollectionViewProperty"/> from.</param>
        /// <returns>MenuItemListCollectionView property value.</returns>
        [AttachedPropertyBrowsableForType( typeof(Window))]
        [AttachedPropertyBrowsableForType( typeof(FrameworkElement))]
        // [AttachedProperty    BrowsableForType(typeof(ItemsControl))]
        public static ICollectionView GetMenuItemListCollectionView(
	        DependencyObject target
        )
        {
	        Logger.Debug($"{nameof(GetMenuItemListCollectionView)} {target}");
	        return (CollectionView)target.GetValue(MenuItemListCollectionViewProperty);
        }

        public static void SetMenuItemListCollectionView(
	        DependencyObject target,
	        ICollectionView  value
        )
        {
	        Logger.Debug($"{nameof(SetMenuItemListCollectionView)} {target}, {value}");
	        target.SetValue(MenuItemListCollectionViewProperty, value);
        }
    }


}
