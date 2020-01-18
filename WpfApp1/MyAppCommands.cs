using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1
{
    public static class MyAppCommands
    {
        public static RoutedUICommand AppSettings =
            new RoutedUICommand("Settings", "AppSettings", typeof(MyAppCommands));

        public static RoutedUICommand NavigateShellItem =
            new RoutedUICommand("Navigate", "NavigateShellItem", typeof(MyAppCommands));

        public static RoutedUICommand OpenWindow =
            new RoutedUICommand("Open Window", nameof(OpenWindow), typeof(MyAppCommands));

    }
}
