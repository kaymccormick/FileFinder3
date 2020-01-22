using Xunit;
using WpfApp1.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Interfaces;

namespace WpfApp1.MenusTests3
{
	public class MenuHelperTests
	{
		[Fact()]
		public void MakeMenuItemTest()
		{
			var header = "test";
			IMenuItem arg = new XMenuItem() { Header = header };
			var item = MenuHelper.MakeMenuItem( arg );
			Assert.NotNull( item );
			Assert.Equal( item.Header, header );


		}
	}
}