﻿using Xunit;
using System;
using System.Collections ;
using System.Collections.Generic;
using System.Diagnostics ;
using System.Linq;
using System.ServiceModel.Channels ;
using System.Windows;
using AppShared ;
using AppShared.Interfaces ;
using Autofac;
using Autofac.Core ;
using Autofac.Core.Activators.Reflection ;
using Common.Logging ;
using Common.Utils ;
using JetBrains.Annotations;
using KayMcCormick.Dev.Test.Metadata ;
using NLog.Config ;
using NLog.Layouts ;
using NLog.Targets ;
using WpfApp1.Windows;
using WpfApp1Tests3.Attributes ;
using WpfApp1Tests3.Fixtures ;
using Xunit.Abstractions ;
using App = WpfApp1.Application.App ;
using LogLevel = NLog.LogLevel ;
using LogManager = NLog.LogManager ;

namespace WpfApp1Tests3
{
    [WpfTestApplication]
    [Collection("WpfApp")]
    [BeforeAfterLogger]
    public class ContainerHelperTests : IClassFixture <LoggingFixture>
    {
	    public LoggingFixture LoggingFixture { get ; }

	    public WpfApplicationFixture WpfApplicationFixture { get ; }

	    public ITestOutputHelper Output { get ; }

	    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        // public ContainerHelperTests ( WpfApplicationFixture WpfApplicationFixture ) { WpfApplicationFixture = WpfApplicationFixture ; }

        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();

	    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
	    public ContainerHelperTests (LoggingFixture loggingFixture, WpfApplicationFixture wpfApplicationFixture, ITestOutputHelper output )
	    {
		    LoggingFixture = loggingFixture ;
		    WpfApplicationFixture = wpfApplicationFixture ;
		    Output = output ;
	    }

	    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        // public ContainerHelperTests ( WpfApplicationFixture WpfApplicationFixture ) { WpfApplicationFixture = WpfApplicationFixture ; }

        [Fact()]
        public void SetupContainerTest()
        {
            IContainer container ;
            var c = ContainerHelper.SetupContainer( out container );
            Logger.Info ( $"{c}" ) ;
            Assert.NotNull(c);
        }
        [Fact()]
        public void SetupContainerTest2()
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured();
	        AddTestLoggingTarget ( nameof ( SetupContainerTest2 ) ) ;
	        Logger.Warn ( "I am here" ) ;
            IContainer container ;
            var c = ContainerHelper.SetupContainer( out container );
            DumpContainer ( container ) ;

           
            
            Assert.NotNull(c);
        }

        private void DumpContainer ( IContainer container )
        {
	        var seen = new HashSet < object > ( ) ;
            #if DUMP1
	        foreach ( var componentRegistryRegistration in container.ComponentRegistry.Registrations )
	        {
		        ContainerHelper.Dump ( componentRegistryRegistration , seen, s => Output.WriteLine(s)) ;
	        }
#endif

	        foreach ( var comp in container.ComponentRegistry.Registrations )
	        {
		        if ( comp.Activator is ReflectionActivator a )
		        {

		        }

		        var s = $"{comp.Activator.LimitType}: " ;
		        foreach ( var service in comp.Services )
		        {
			        if ( service is TypedService ts )
			        {
				        Output.WriteLine( $"service {s} - {ts.ServiceType}" ) ;
			        }
			        else
			        {
				        Output.WriteLine( $"service is {service}" ) ;
			        }
		        }
	        }
        }
        private void AddTestLoggingTarget ( string setupContainerTest2Name )
        {
            AppLoggingConfigHelper.EnsureLoggingConfigured(true, LogMethod);
            FileTarget fileTarget = new FileTarget ( "test target" ) ;
            fileTarget.FileName = Layout.FromString ( "test-" + setupContainerTest2Name + ".txt" ) ;
            LogManager.LogFactory.Configuration.AddTarget ( fileTarget ) ;
            LogManager.LogFactory.Configuration.LoggingRules.Insert(0, new LoggingRule("*", LogLevel.Trace, fileTarget) );
            LogManager.LogFactory.ReconfigExistingLoggers (  );


        }

        private void LogMethod ( string message )
        {
            Debug.WriteLine ( message);
	        Output.WriteLine(message);
        }


        [Fact()]
        [ UsedImplicitly ]
        public void ContainerTestResolveIMenuItemList()
        {
            IContainer container ;
            var c = ContainerHelper.SetupContainer( out container );
            DumpContainer(container);
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
            
	        var customAttribute = Attribute.GetCustomAttribute ( this.GetType().GetMethod(nameof(ResolveMainWindow)), typeof ( AppInitializeAttribute ) )  as AppInitializeAttribute;
	        customAttribute.MyApp = WpfApplicationFixture.MyApp as App ;

	        Assert.NotNull ( WpfApplicationFixture ) ;
            Assert.NotNull ( WpfApplicationFixture.BasePackUri ) ;
            var uri = new Uri ( WpfApplicationFixture.BasePackUri , "Application/App.xaml" ) ;
Logger.Info(uri);

foreach ( DictionaryEntry resource in ( WpfApplicationFixture.MyApp as WpfApp1.Application.App )
   .Resources )
{
	Logger.Info ( $"{resource.Key}" ) ;
}
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
