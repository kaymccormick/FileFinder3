using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using SharpShell.Interop;
using Shell32;
using Vanara.Windows.Shell;
using HWND = Vanara.PInvoke.HWND;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public App()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<SystemParametersControl>().As<ISettingsPanel>();

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
/*            var shellFolder = ShellFolder.Desktop;
            Logger.Debug(shellFolder.GetDisplayName(ShellItemDisplayString.NormalDisplay));
            var enumerateChildren = shellFolder.EnumerateChildren(FolderItemFilter.NonFolders);
            Logger.Debug(enumerateChildren.Count());
            foreach(var item in enumerateChildren)
            {
                Logger.Debug(item.Name);
            }
	    */
            if (e.Args.Any())
            {
                var windowName = e.Args[0];
                var xaml = windowName + ".xaml";
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Uri));
                if (converter.CanConvertFrom(typeof(string)))
                {
                    StartupUri = (Uri) converter.ConvertFrom(xaml);
                    Logger.Debug("Startup URI is {startupUri}", StartupUri);
                }
            }
        }
    }
}
