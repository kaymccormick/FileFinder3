using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NLog;

namespace WpfApp1.CollectionViews
{
    public static class MenuItemListCollectionViewProperties
    {

        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();


        public static readonly DependencyProperty MenuItemListCollectionViewProperty =
            DependencyProperty.RegisterAttached( "MenuItemListCollectionView",
                                                 typeof(CollectionView), typeof(MenuItemListCollectionViewProperties),
                                                 new FrameworkPropertyMetadata( null,
                                                                                FrameworkPropertyMetadataOptions.Inherits));

        // [AttachedPropertyBrowsableForType(typeof(Window))]
        // [AttachedProperty    BrowsableForType(typeof(ItemsControl))]
        [AttachedPropertyBrowsableForType( typeof(DependencyObject))]
        [AttachedPropertyBrowsableForType( typeof(object))]
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = true)]
        public static CollectionView GetMenuItemListCollectionView(
            DependencyObject target
        )
        {
            Logger.Debug( $"{nameof( GetMenuItemListCollectionView )} {target}" );
            return (CollectionView)target.GetValue( MenuItemListCollectionViewProperty );
        }

        public static void SetMenuItemListCollectionView(
            DependencyObject target,
            CollectionView   value
        )
        {
            Logger.Debug( $"{nameof( SetMenuItemListCollectionView )} {target}, {value}" );
            target.SetValue( MenuItemListCollectionViewProperty, value );
        }
    }
}
