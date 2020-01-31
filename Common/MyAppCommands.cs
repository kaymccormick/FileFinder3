using System.Windows.Input ;

namespace Common
{
	public static class MyAppCommands
	{
		public static readonly RoutedUICommand AppSettings =
			new RoutedUICommand ( "Settings" , nameof ( AppSettings ) , typeof ( MyAppCommands ) ) ;

		public static readonly RoutedUICommand NavigateShellItem =
			new RoutedUICommand (
			                     "Navigate"
			                   , nameof ( NavigateShellItem )
			                   , typeof ( MyAppCommands )
			                    ) ;

		public static readonly RoutedUICommand OpenWindow =
			new RoutedUICommand (
			                     "Open Window"
			                   , nameof ( OpenWindow )
			                   , typeof ( MyAppCommands )
			                    ) ;

		public static readonly RoutedUICommand QuitApplication =
			new RoutedUICommand (
			                     "Quit Application"
			                   , nameof ( QuitApplication )
			                   , typeof ( MyAppCommands )
			                    ) ;

		public static readonly RoutedUICommand VisitTypeCommand =
			new RoutedUICommand (
			                     "Visit Type"
			                   , nameof ( VisitTypeCommand )
			                   , typeof ( MyAppCommands )
			                    ) ;

		public static readonly RoutedUICommand LoadAssemblyList =
			new RoutedUICommand (
			                     "Load"
			                   , nameof ( LoadAssemblyList )
			                   , typeof ( MyAppCommands )
			                    ) ;

		public static readonly RoutedUICommand Restart =
			new RoutedUICommand ( "Restart" , nameof ( Restart ) , typeof ( MyAppCommands ) ) ;

		public static readonly RoutedUICommand DumpDebug =
			new RoutedUICommand ( "Dump Debug" , nameof ( DumpDebug ) , typeof ( MyAppCommands ) ) ;

		public static readonly RoutedUICommand FilterInstances =
			new RoutedUICommand (
			                     "FIlter Instances"
			                   , nameof ( FilterInstances )
			                   , typeof ( MyAppCommands )
			                    ) ;

		public static readonly RoutedUICommand Load =
			new RoutedUICommand ( "Load" , nameof ( Load ) , typeof ( MyAppCommands ) ) ;

		public static readonly RoutedUICommand Metadata =
			new RoutedUICommand ( "Metadata" , nameof ( Metadata ) , typeof ( MyAppCommands ) ) ;

		public static readonly RoutedUICommand VisitType =
			new RoutedUICommand ( "VisitType" , nameof ( VisitType ) , typeof ( MyAppCommands ) ) ;
	}
}