using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Windows ;
using AppShared.Interfaces ;
using Autofac.Features.Metadata ;
using NLog ;

namespace Common.Menus
{
	/// <summary>
	/// Definition of primary top level menu
	/// </summary>
	public class WindowsTopLevelMenu : ITopLevelMenu
	{
		private readonly IMenuItem          _xMenuItem ;
		private readonly Func < IMenuItem > _xMenuItemCreator ;

		private readonly ILogger Logger ;

		public WindowsTopLevelMenu (
			IEnumerable < Meta < Lazy < Window > > > windows
		  , Func < IMenuItem >                       xMenuItemCreator
		  , Func < Type , ILogger >                  func
		)
		{
			Logger = func ( typeof ( WindowsTopLevelMenu ) ) ;

			_xMenuItemCreator = xMenuItemCreator ;
			_xMenuItem        = xMenuItemCreator ( ) ;
			Windows           = windows ;
		}

		public IEnumerable < Meta < Lazy < Window > > > Windows { get ; }

		public IMenuItem GetXMenuItem ( )
		{
			var root = _xMenuItem ;
			_xMenuItem.Header   = "Windows" ;
			_xMenuItem.Children = Windows.Select ( Selector ).ToList ( ) ;
			return root ;
		}

		private IMenuItem Selector ( Meta < Lazy < Window > > window , int i )
		{
			var m = _xMenuItemCreator ( ) ;
			if ( window.Metadata.ContainsKey ( "WindowTitle" ) == false )
			{
				Logger.Warn ( $"No window title for window {window}" ) ;
				m.Header = window.Value.ToString ( ) ;
			}
			else
			{
				m.Header = ( string ) window.Metadata[ "WindowTitle" ] ;
			}

			m.Command          = MyAppCommands.OpenWindow ;
			m.CommandParameter = window ;
			return m ;
		}
	}
}