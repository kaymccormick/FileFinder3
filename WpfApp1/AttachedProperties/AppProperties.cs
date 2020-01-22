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


        public static readonly DependencyProperty MenuItemListCollectionViewProperty =
	        DependencyProperty.RegisterAttached("MenuItemListCollectionView",
	                                            typeof(ICollectionView), typeof(AppProperties));

        // [AttachedPropertyBrowsableForType(typeof(Window))]
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
