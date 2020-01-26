﻿using System.Windows.Input;

namespace WpfApp1.Commands
{
    public static class MyAppCommands
    {
        public static readonly RoutedUICommand AppSettings =
            new RoutedUICommand(
                                "Settings", nameof( AppSettings ),
                                typeof(MyAppCommands)
                               );

        public static readonly RoutedUICommand NavigateShellItem =
            new RoutedUICommand(
                                "Navigate", nameof( NavigateShellItem ),
                                typeof(MyAppCommands)
                               );

        public static readonly RoutedUICommand OpenWindow =
            new RoutedUICommand(
                                "Open Window", nameof( OpenWindow ),
                                typeof(MyAppCommands)
                               );

        public static readonly RoutedUICommand QuitApplication =
	        new RoutedUICommand( "Quit Application", nameof( QuitApplication ),
	                             typeof(MyAppCommands) );
        public static readonly RoutedUICommand VisitTypeCommand =
	        new RoutedUICommand( "Visit Type", nameof( VisitTypeCommand ),
	                             typeof(MyAppCommands) );

        public static readonly RoutedUICommand LoadAssemblyList =
	        new RoutedUICommand( "Load", nameof( LoadAssemblyList ),
	                             typeof(MyAppCommands) );
        public static readonly RoutedUICommand Restart =
	        new RoutedUICommand( "Restart", nameof( Restart),
	                             typeof(MyAppCommands) );
        public static readonly RoutedUICommand DumpDebug =
	        new RoutedUICommand( "Dump Debug", nameof( DumpDebug),
	                             typeof(MyAppCommands) );
        public static readonly RoutedUICommand FilterInstances =
	        new RoutedUICommand( "Dump Debug", nameof( DumpDebug),
	                             typeof(MyAppCommands) );
        

    }
}