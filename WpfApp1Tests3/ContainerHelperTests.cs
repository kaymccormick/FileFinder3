using Xunit;
using WpfApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Menus;
using WpfApp1.Util;
using Autofac;
using JetBrains.Annotations;
using WpfApp1.Interfaces;
using WpfApp1.Windows;

namespace WpfApp1Tests3
{
    public class ContainerHelperTests
    {
        [Fact()]
        public void SetupContainerTest()
        {
            var c = ContainerHelper.SetupContainer();
            Assert.NotNull(c);
        }

        [Fact()]
        [ UsedImplicitly ]
        public void ContainerTest_ResolveIMenuItemList()
        {
            var c = ContainerHelper.SetupContainer();
            var menuItemList = c.Resolve<IMenuItemList>();
            Assert.NotNull(menuItemList);

            if (!menuItemList.First().Children.Any())
            {
	            throw new Exception("Empty menu window list");
            }



        }

        [Fact]
        public void ResolveWindows()
        {
	        var c = ContainerHelper.SetupContainer();
	        var enumerable = c.Resolve < IEnumerable < Lazy < Window > > >();
            Assert.NotEmpty(enumerable);
        }

        [ WpfFact ]
        public void ResolveMainWindow()

        {
	        var c = ContainerHelper.SetupContainer();
	        var mainWindow = c.Resolve < MainWindow >();
            Assert.NotNull(mainWindow);
        }
    }

    
}