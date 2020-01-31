using System.Linq ;
using System.Windows ;
using System.Windows.Controls ;

namespace Common.Menus
{
	public class MenuItemContainerStyleSelector : StyleSelector
	{
		public override Style SelectStyle ( object item , DependencyObject container )
		{
			if ( item is XMenuItem x )
			{
				if ( x.Children.Any ( ) )
				{
					if ( container is FrameworkElement ic )
					{
						return ic.FindResource ( "MenuItemWithChildren" ) as Style ;
					}
				}
				else
				{
					if ( container is FrameworkElement ic )
					{
						return ic.FindResource ( "MenuItemNoChildren" ) as Style ;
					}
				}
			}

			return base.SelectStyle ( item , container ) ;
		}
	}
}