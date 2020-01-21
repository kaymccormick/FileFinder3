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
            Logger.Debug( $"{item} {container}" );
            if ( item is IMenuItem x )
            {
                Logger.Debug( $"item is IMEnuITem" );
                if ( x.Children.Any() )
                {
                    if ( container is FrameworkElement ic )
                    {
                        var key = "Menu_ItemTemplateChildren";
                        var dataTemplate =
                            ic.FindResource( key ) as DataTemplate;
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
                    if ( container is FrameworkElement ic )
                    {
                        var key = "Menu_ItemTemplateNoChildren";
                        var dataTemplate =
                            ic.FindResource( key ) as DataTemplate;
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
            }

            Logger.Debug("Returning result from base method");
            return base.SelectTemplate( item, container );
        }
    }
}
