using System.IO;
using System.Linq.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using NLog;
using WpfApp1.Interfaces;

namespace WpfApp1.Menus
{
    public class MenuMenuItemTemplateSelector : DataTemplateSelector

    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public override DataTemplate SelectTemplate(
            object           item,
            DependencyObject container
        )
        {
            var menuItem = container as MenuItem;
            var cp = container as ContentPresenter;
            if ( menuItem == null )
            {
                Logger.Warn( $"container is not a menuitem {container.GetType()}" );
                return base.SelectTemplate( item, container );
            }

            var source = item as IMenuItem;
            if ( source == null )
            {
                Logger.Warn( "item is not a IMenuItem" );
                return base.SelectTemplate( item, container );
            }

            Logger.Info( $"{menuItem}" );
            Logger.Debug( $"{item} {container}" );
            Logger.Debug( item.GetType() );
            if ( item is IMenuItem x )
            {
                Logger.Debug( $"item is IMenuItem" );
                if ( x.Children.Any() )
                {
                    var key = "Menu_ItemTemplateChildren";
                    var dataTemplate =
                        menuItem.FindResource( key ) as DataTemplate;
                    Logger.Debug(
                                 $"returning {key} {dataTemplate.DataTemplateKey}"
                                );
#if writexaml
                        var sw = new StringWriter();
                        XamlWriter.Save( dataTemplate, sw );
                        Logger.Trace( sw.ToString() );
#endif
                    return dataTemplate;
                }
            }
            else
            {
                var key = "Menu_ItemTemplateNoChildren";
                var dataTemplate =
                    menuItem.FindResource( key ) as DataTemplate;
                Logger.Debug(
                             $"returning {key} {dataTemplate.DataTemplateKey}"
                            );
#if writexaml
                        var sw = new StringWriter();
                        XamlWriter.Save( dataTemplate, sw );
                        Logger.Trace( sw.ToString() );
#endif
                return dataTemplate;
            }

            Logger.Debug("Returning result from base method");
            return base.SelectTemplate( item, container );
        }
    }
}
