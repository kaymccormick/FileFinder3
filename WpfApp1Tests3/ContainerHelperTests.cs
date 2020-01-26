using Xunit;
using WpfApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AppShared.Interfaces ;
using WpfApp1.Menus;
using WpfApp1.Util;
using Autofac;
using AutoFixture ;
using JetBrains.Annotations;
using KayMcCormick.Dev.Test.Metadata ;
using NLog ;
using WpfApp1.Windows;
using WpfApp1Tests3.Attributes ;
using WpfApp1Tests3.Fixtures ;

namespace WpfApp1Tests3
{
    [WpfTestApplication]
    public class ContainerHelperTests
    {
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
	    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
	    public ContainerHelperTests ( WpfApplicationFixture myFixture ) { MyFixture = myFixture ; }

	    [Fact()]
        public void SetupContainerTest()
        {
	        IContainer container ;
	        var c = ContainerHelper.SetupContainer( out container );
            Assert.NotNull(c);
        }

        public WpfApplicationFixture MyFixture { get ; set ; }

        [Fact()]
        [ UsedImplicitly ]
        public void ContainerTestResolveIMenuItemList()
        {
	        IContainer container ;
	        var c = ContainerHelper.SetupContainer( out container );
            var menuItemList = c.Resolve<IMenuItemList>();
            Assert.NotNull(menuItemList);
            Assert.NotEmpty ( menuItemList.First ( ).Children ) ;
          

        }

        [Fact]
        public void ResolveWindows()
        {
	        IContainer container ;
	        var c = ContainerHelper.SetupContainer( out container );
	        var enumerable = c.Resolve < IEnumerable < Lazy < Window > > >();
	        var l = enumerable.ToList ( ) ;
            Assert.NotEmpty(l);
            Assert.Equal( 3 , l.Count ) ;
        }

        [ WpfFact ]
        [AppInitialize]
        public void ResolveMainWindow() {

        var uri = new Uri ( MyFixture.BasePackUri , "Application/App.xaml" ) ;
Logger.Info(uri);

	        IContainer container ;
	        var c = ContainerHelper.SetupContainer( out container );
	        var mainWindow = c.Resolve < MainWindow >();
            Assert.NotNull(mainWindow);
        }

        [ WpfFact ]
        public void TestPushContext ( )
        {
	        IContainer container ;
	        var c = ContainerHelper.SetupContainer ( out container ) ;
        }
    }

    
}
