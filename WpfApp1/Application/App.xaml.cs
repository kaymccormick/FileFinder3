﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Scripting.Utils;
using NLog;
using WpfApp1.AttachedProperties;
using WpfApp1.Commands;
using WpfApp1.DataSource;
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
        public MenuItemList MyMenuItemList { get; private set; }

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
	        
	        AddEventListeners();
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

            Dispatcher.BeginInvoke(DispatcherPriority.Send, (DispatcherOperationCallback)delegate {
// var windows = AppContainer.Resolve < IEnumerable < Lazy < Window > > >();
                // windows.Select( (
                //                     lazy,
                //                     i
                //                 ) => {
                //                     var cmdBinding = new CommandBinding( MyAppCommands.OpenWindow, OpenWindowExecuted );
                //                     CommandManager.RegisterClassCommandBinding( typeof(Window), cmdBinding );
                //                     return true;
                //                 } );
                var menuItemList = AppContainer.Resolve <IMenuItemList>();
                if ( ! menuItemList.First().Children.Any() )
                {
	                throw new Exception("Empty menu window list");
                }
                var tryFindResource = TryFindResource("Ds1") as MyDS1;
                CollectionView x = new ListCollectionView(menuItemList);
                //MenuItemListCollectionViewProperties.SetMenuItemListCollectionView( this, x );
                RoutedEventHandler handler = new RoutedEventHandler((o, args) => AppProperties.SetMenuItemListCollectionView((FrameworkElement)o, x));
                EventManager.RegisterClassHandler(typeof(Window), FrameworkElement.LoadedEvent, handler);
                Resources["MyMenuItemList"] = menuItemList;
#if SHOWWINDOW
                var mainWindow = new MainWindow();
                mainWindow.Show();
#endif
                return null;
            }, null );
        }

        private void AddEventListeners()
        {
	        try
	        {
		        EventManager.RegisterClassHandler( typeof(Window), UIElement.KeyDownEvent,
		                                           new KeyEventHandler( OnKeyDown ) );
	        }
	        catch ( Exception ex )
	        {
		        Logger.Error( ex, ex.Message );
	        }
        }

        private void OnKeyDown(
	        object       sender,
	        KeyEventArgs e
        )
        {
	        if ( e.Key == Key.T && e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt) )
	        {
		        Process.Start( new ProcessStartInfo( @".\Demo.XamlDesigner.exe", @"..\WpfApp1\Windows\MainWindow.xaml" )
		                       { WorkingDirectory = @"..\..\..\tools" } );
	        }
        }

        // private void WindowLoaded(
        //     object          sender,
        //     RoutedEventArgs e
        // )
        // {
        //     Window w = sender as Window;
        //     if ( w == null )
        //     {
        //         Logger.Warn( $"Received WindowLoaded from non-Window {sender}"  );
        //         return;
        //     }
        //     MenuItemListCollectionViewProperties.SetMenuItemListCollectionView(w, );
        // }
    }
}
