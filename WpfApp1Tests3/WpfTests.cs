using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NLog;
using WpfApp1.Menus;
using Xunit;

namespace WpfApp1Tests3
{
    [Collection("WpfApp")]
    public class WpfTests : IClassFixture <ContainerFixture>
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public GenericApplicationFixture fixture;
        private readonly ContainerFixture _containerFixture;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WpfTests(
            GenericApplicationFixture fixture,
            ContainerFixture containerFixture
        )
        {
            this.fixture = fixture;
            _containerFixture = containerFixture;
        }

        [Fact]
        public void Test1()
        {
            using (var scope = _containerFixture.LifetimeScope.BeginLifetimeScope())
            {
                var menuItemList = scope.Resolve < MenuItemList >();
                Assert.NotNull(menuItemList);
                Assert.NotEmpty(menuItemList);
                fixture.MyApp.Resources["MyMenuItemList"] = menuItemList;
                var found = fixture.MyApp.FindResource( "MyMenuItemList" );
                Assert.NotNull( found );
                var x = String.Join( ", ", menuItemList.First().Children );
                Logger.Debug( $"found {found}, {x}" );
            }
        }

    }
}
