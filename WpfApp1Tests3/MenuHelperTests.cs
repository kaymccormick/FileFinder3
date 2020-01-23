using System;
using Autofac;
using NLog;
using WpfApp1.Interfaces;
using WpfApp1.Menus;
using WpfApp1Tests3.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace WpfApp1Tests3
{
	[Collection( "WpfApp")]
	public class MenuHelperTests : WpfTestsBase
	{
		private static readonly Logger Logger =
			LogManager.GetCurrentClassLogger();

		private Func < IMenuItem > _xMenuItemCreator;

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
			_xMenuItemCreator = containerScope.Resolve<Func<IMenuItem>>();
		}

		[WpfFact()]
		public void MakeMenuItemTest()
		{
			var header = "test";
			IMenuItem arg = _xMenuItemCreator();
			arg.Header = header;
			Assert.NotNull(arg);
			var item = MenuHelper.MakeMenuItem( arg );
			Assert.NotNull( item );
			Assert.Equal(header, item.Header );


		}
	}
}