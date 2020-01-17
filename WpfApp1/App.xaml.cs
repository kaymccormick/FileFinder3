using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SharpShell.Interop;
using Shell32;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IShellFolder desktopFolder;
            if(SharpShell.Interop.Shell32.SHGetDesktopFolder(out desktopFolder) < 0)
            {
                throw new Exception();
            }

            Console.WriteLine(desktopFolder);
        }
    }
}
