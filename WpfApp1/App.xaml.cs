using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var shellFolder = ShellFolder.Desktop;
            Logger.Debug(shellFolder.GetDisplayName(ShellItemDisplayString.NormalDisplay));
            var enumerateChildren = shellFolder.EnumerateChildren(FolderItemFilter.NonFolders);
            Logger.Debug(enumerateChildren.Count());
            foreach(var item in enumerateChildren)
            {
                Logger.Debug(item.Name);
            }
        }
    }
}
