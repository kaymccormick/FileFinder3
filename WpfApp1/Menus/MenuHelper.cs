using System.Linq ;
using System.Windows.Controls ;
using AppShared.Interfaces ;

namespace WpfApp1.Menus
{
	public static class MenuHelper
	{
		public static MenuItem MakeMenuItem ( IMenuItem arg )
		{
			var r = new MenuItem { Header = arg.Header } ;
			if ( arg.Children != null )
			{
				var menuItems = arg.Children.Select ( MakeMenuItem ) ;
				foreach ( var menuItem in menuItems )
				{
					r.Items.Add ( menuItem ) ;
				}
			}
			else
			{
				r.Command          = arg.Command ;
				r.CommandParameter = arg.CommandParameter ;
				r.CommandTarget    = arg.CommandTarget ;
			}

			return r ;
		}
	}
}