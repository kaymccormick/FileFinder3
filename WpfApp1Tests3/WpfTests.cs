using System ;
using System.Linq ;
using System.Windows ;
using System.Windows.Baml2006 ;
using System.Windows.Markup ;
using AppShared.Interfaces ;
using Autofac ;
using AutoFixture ;
using NLog ;
using TestLib ;
using TestLib.Attributes ;
using TestLib.Fixtures ;
using WpfApp1Tests3.Fixtures ;
using Xunit ;
using Xunit.Abstractions ;

namespace WpfApp1Tests3
{
    /// <summary>
    /// 
    /// </summary>
    [ Collection ( "WpfApp" ) ]
    [LogTestMethod, BeforeAfterLogger]
    public class WpfTests : WpfTestsBase
    {
        public WpfTests (
            WpfApplicationFixture fixture
          , AppContainerFixture containerFixture
          , UtilsContainerFixture utilsContainerFixture
          , ITestOutputHelper outputHelper
        ) : base (
                  fixture
                , containerFixture
                , utilsContainerFixture
                , outputHelper
                 )
        {
            Logger.Debug ( $"{nameof ( WpfTests )} constructor" ) ;
        }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        private void DoLog ( string test )
        {
            // LB ( ).Level ( LogLevel.Trace ).Message ( test ).Write ( ) ;
        }

        [ Fact ]
        [ Trait ( "Experimental" , "true" ) ]
        public void Test1 ( )
        {
            var menuItemList = containerScope.Resolve < IMenuItemList > ( ) ;
            Assert.NotNull ( menuItemList ) ;
            Assert.NotEmpty ( menuItemList ) ;

            if ( Fixture.MyApp != null )
            {
                Fixture.MyApp.Resources[ "MyMenuItemList" ] = menuItemList ;
                var found = Fixture.MyApp.FindResource ( "MyMenuItemList" ) ;
                Assert.NotNull ( found ) ;
                var x = string.Join ( ", " , menuItemList.First ( ).Children ) ;
                Logger.Debug ( $"found {found}, {x}" ) ;
            }

            var uri = new Uri ( Fixture.BasePackUri , ResourcesPaths.MenuResourcesPath ) ;
            Logger.Debug ( $"{uri}" ) ;

            var stream = Application.GetResourceStream ( uri ) ;
            Logger.Info ( stream.ContentType ) ;
            var baml2006Reader = new Baml2006Reader ( stream.Stream ) ;


            var o = XamlReader.Load ( baml2006Reader ) ;
            Assert.NotNull ( o ) ;
            ///var o = Application.LoadComponent( uri );
            var menuResources = o as ResourceDictionary ;
            Assert.NotNull ( menuResources ) ;
            //var stack = InstanceFactory.CreateContextStack < InfoContext >();
            var stack = MyStack ;
            var entry = myServices.InfoContextFactory ( nameof ( menuResources ) , menuResources ) ;
            stack.Push ( entry ) ;

            foreach ( var q in menuResources.Keys )
            {
                var resource = menuResources[ q ] ;
                stack.Push ( myServices.InfoContextFactory ( "key" , q ) ) ;
                Logger.Debug ( $"{q}: {resource}" ) ;
                var prefix = $"Resource[{q}]" ;
                DumpHelper.DumpResource ( stack , resource , myServices.InfoContextFactory ) ;
                stack.Pop ( ) ;
            }
        }

    }

#pragma warning disable 1591
}