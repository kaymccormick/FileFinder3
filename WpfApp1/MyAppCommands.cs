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
        public static readonly RoutedUICommand AppSettings =
            new RoutedUICommand("Settings", nameof(AppSettings), typeof(MyAppCommands));

        public static readonly RoutedUICommand NavigateShellItem =
            new RoutedUICommand("Navigate", nameof(NavigateShellItem), typeof(MyAppCommands));

        public static readonly RoutedUICommand OpenWindow =
            new RoutedUICommand("Open Window", nameof(OpenWindow), typeof(MyAppCommands));

    }
}
