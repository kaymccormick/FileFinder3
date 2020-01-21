using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Autofac;
using Autofac.Extras.DynamicProxy;
using NLog;
using WpfApp1.Commands;
using WpfApp1.Interfaces;
using WpfApp1.Menus;
using WpfApp1.Util;
using IContainer = Autofac.IContainer;

namespace WpfApp1.Application
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public App()
        {
            AppContainer = ContainerHelper.SetupContainer();
        }


        public IContainer AppContainer { get; set; }

        private void OpenWindowExecuted(
            object                  sender,
            ExecutedRoutedEventArgs e
        )
        {
            Logger.Info( $"{sender} {e.Parameter}" );
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "VSTHRD001:Avoid legacy thread switching APIs", Justification = "<Pending>")]
        private void ApplicationStartup(
            object           sender,
            StartupEventArgs e
        )
        {
            if ( e.Args.Any() )
            {
                var windowName = e.Args[0];
                var xaml = windowName + ".xaml";
                var converter = TypeDescriptor.GetConverter( typeof(Uri) );
                if ( converter.CanConvertFrom( typeof(string) ) )
                {
                    StartupUri = (Uri)converter.ConvertFrom( xaml );
                    Logger.Debug( "Startup URI is {startupUri}", StartupUri );
                }
            }

            Dispatcher.BeginInvoke( DispatcherPriority.Send, (DispatcherOperationCallback)delegate {
                // var windows = AppContainer.Resolve < IEnumerable < Lazy < Window > > >();
                // windows.Select( (
                //                     lazy,
                //                     i
                //                 ) => {
                //                     var cmdBinding = new CommandBinding( MyAppCommands.OpenWindow, OpenWindowExecuted );
                //                     CommandManager.RegisterClassCommandBinding( typeof(Window), cmdBinding );
                //                     return true;
                //                 } );
                var menuItemList = AppContainer.Resolve < IMenuItemList >();
                Resources["MyMenuItemList"] = menuItemList;
#if SHOWWINDOW
                var mainWindow = new MainWindow();
                mainWindow.Show();
#endif
                return null;
            }, null );
        }
    }
}