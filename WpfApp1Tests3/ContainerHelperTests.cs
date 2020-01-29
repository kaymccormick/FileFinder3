using Xunit;
using WpfApp1;
using System;
using System.Collections.Generic;
using System.Diagnostics ;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AppShared.Interfaces ;
using WpfApp1.Menus;
using WpfApp1.Util;
using Autofac;
using Autofac.Core ;
using Autofac.Core.Activators.Reflection ;
using AutoFixture ;
using Common.Logging ;
using Common.Utils ;
using DynamicData ;
using JetBrains.Annotations;
using KayMcCormick.Dev.Test.Metadata ;
using NLog.Config ;
using NLog.Layouts ;
using NLog.Targets ;
using WpfApp1.Windows;
using WpfApp1Tests3.Attributes ;
using WpfApp1Tests3.Fixtures ;
using LogLevel = NLog.LogLevel ;
using LogManager = NLog.LogManager ;

namespace WpfApp1Tests3
{
    [WpfTestApplication]

    public class ContainerHelperTests
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        // public ContainerHelperTests ( WpfApplicationFixture myFixture ) { MyFixture = myFixture ; }

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        // public ContainerHelperTests ( WpfApplicationFixture myFixture ) { MyFixture = myFixture ; }

        [Fact()]
        public void SetupContainerTest()
        {
            IContainer container ;
            var c = ContainerHelper.SetupContainer( out container );
            Assert.NotNull(c);
        }
        [Fact()]
        public void SetupContainerTest2()
        {
	        AddTestLoggingTarget ( nameof ( SetupContainerTest2 ) ) ;
            IContainer container ;
            var c = ContainerHelper.SetupContainer( out container );
            var seen = new HashSet < object > ( ) ;
            foreach ( var componentRegistryRegistration in c.ComponentRegistry.Registrations )
            {
                ContainerHelper.Dump ( componentRegistryRegistration , seen ) ;
            }

            foreach ( var comp in container.ComponentRegistry.Registrations
            )
            {
	            if ( comp.Activator is ReflectionActivator a )
	            {
                    
	            }

	            var s = $"{comp.Activator.LimitType}: " ;
                foreach ( var service in comp.Services )
                {
                    if ( service is TypedService ts )
                    {
                        Logger.Info ( $"service {s} - {ts.ServiceType}" ) ;
                    }
                    else
                    {
                        Logger.Info ( $"service is {service}" ) ;
                    }
                }
            }
            Assert.NotNull(c);
        }

        private void AddTestLoggingTarget ( string setupContainerTest2Name )
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured(true);
            FileTarget fileTarget = new FileTarget ( "test target" ) ;
            fileTarget.FileName = Layout.FromString ( "test-" + setupContainerTest2Name + ".txt" ) ;
            LogManager.LogFactory.Configuration.AddTarget ( fileTarget ) ;
            LogManager.LogFactory.Configuration.LoggingRules.Insert(0, new LoggingRule("*", LogLevel.Trace, fileTarget) );
            LogManager.LogFactory.ReconfigExistingLoggers (  );


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
            Assert.NotEmpty(menuItemList);
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
        public void ResolveMainWindow()
        {
            Assert.NotNull ( MyFixture ) ;
            Assert.NotNull ( MyFixture.BasePackUri ) ;
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

        [ Fact ]
        public void TestResolveTopLevelMenu ( )
        {
	        IContainer container ;
	        var c = ContainerHelper.SetupContainer ( out container ) ;
	        var topLevelMenus = c.Resolve < IEnumerable < ITopLevelMenu > > ( ).ToList ( ) ;
            Assert.NotEmpty(topLevelMenus);
	        foreach ( var topLevelMenu in topLevelMenus )
	        {
		        Logger.Debug ( $"{topLevelMenu.GetXMenuItem ( ).Header}" ) ;
	        }

        }
    }

    
}
