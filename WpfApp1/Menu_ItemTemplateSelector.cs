using System.IO;
using System.Linq.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WpfApp1
{
    public class Menu_ItemTemplateSelector : DataTemplateSelector

    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Logger.Debug($"{item} {container}");
            if (item is XMenuItem x)
            {
                if (x.Children.Any())
                {
                    if (container is FrameworkElement ic)
                    {
                        var key = "Menu_ItemTemplateChildren";
                        var dataTemplate = ic.FindResource(key) as DataTemplate;
                        Logger.Debug($"returning {key} {dataTemplate.DataTemplateKey}");
                        var sw = new StringWriter();
                        XamlWriter.Save(dataTemplate, sw);
                        Logger.Info(sw.ToString());
                        return dataTemplate;
                    }
                }
                else
                {
                    if (container is FrameworkElement ic)
                    {
                        var key = "Menu_ItemTemplateNoChildren";
                        var dataTemplate = ic.FindResource(key) as DataTemplate;
                        Logger.Debug($"returning {key} {dataTemplate.DataTemplateKey}");
                        var sw = new StringWriter();
                        XamlWriter.Save(dataTemplate, sw);
                        Logger.Info(sw.ToString());
                        return dataTemplate;
                    }
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}