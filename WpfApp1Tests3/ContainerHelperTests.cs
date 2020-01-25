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
        public void ContainerTestResolveIMenuItemList()
        {
            var c = ContainerHelper.SetupContainer();
            var menuItemList = c.Resolve<IMenuItemList>();
            Assert.NotNull(menuItemList);
            Assert.NotEmpty ( menuItemList.First ( ).Children ) ;
          

        }

        [Fact]
        public void ResolveWindows()
        {
	        var c = ContainerHelper.SetupContainer();
	        var enumerable = c.Resolve < IEnumerable < Lazy < Window > > >();
	        var l = enumerable.ToList ( ) ;
            Assert.NotEmpty(l);
            Assert.Equal( 3 , l.Count ) ;
        }

        [ WpfFact ]
        public void ResolveMainWindow()

        {
	        var c = ContainerHelper.SetupContainer();
	        var mainWindow = c.Resolve < MainWindow >();
            Assert.NotNull(mainWindow);
        }

        [ WpfFact ]
        public void TestPushContext ( )
        {
	        var c = ContainerHelper.SetupContainer ( ) ;

        }
    }

    
}