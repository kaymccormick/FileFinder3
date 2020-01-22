using Xunit;
using WpfApp1.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Interfaces;
using WpfApp1Tests3;
using WpfApp1Tests3.Fixtures;
using Xunit.Abstractions;

namespace WpfApp1.MenusTests3
{
	[Collection( "WpfApp")]
	public class MenuHelperTests : WpfTestsBase
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public MenuHelperTests(
			WpfApplicationFixture fixture,
			ContainerFixture      containerFixture,
			ObjectIDFixture       objectIdFixture,
			UtilsContainerFixture utilsContainerFixture,
			ITestOutputHelper     outputHelper
		) : base( fixture, containerFixture, objectIdFixture, utilsContainerFixture, outputHelper )
		{
		}

		[WpfFact()]
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