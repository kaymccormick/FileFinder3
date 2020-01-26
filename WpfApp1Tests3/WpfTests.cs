using System ;
using System.Linq ;
using System.Windows ;
using System.Windows.Baml2006 ;
using System.Windows.Markup ;
using AppShared.Interfaces ;
using Autofac ;
using NLog ;
using WpfApp1.Attributes ;
using WpfApp1.Util ;
using WpfApp1Tests3.Attributes ;
using WpfApp1Tests3.Fixtures ;
using WpfApp1Tests3.Utils ;
using Xunit ;
using Xunit.Abstractions ;

namespace WpfApp1Tests3
{
	[ Collection ( "WpfApp" ) ]
	public class WpfTests : WpfTestsBase
	{
		public WpfTests (
			WpfApplicationFixture fixture
		  , ContainerFixture      containerFixture
		  , ObjectIDFixture       objectIdFixture
		  , UtilsContainerFixture utilsContainerFixture
		  , ITestOutputHelper     outputHelper
		) : base (
		          fixture
		        , containerFixture
		        , objectIdFixture
		        , utilsContainerFixture
		        , outputHelper
		         )
		{
			Logger.Debug ( $"{nameof ( WpfTests )} constructor"  ) ;
		}

		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		private void DoLog ( string test )
		{
			LB ( ).Level ( LogLevel.Trace ).Message ( test ).Write ( ) ;
		}

		[ Fact ]
		[ Trait ( "Experimental" , "true" ) ]
		public void Test1 ( )
		{
			var menuItemList = containerScope.Resolve < IMenuItemList > ( ) ;
			Assert.NotNull ( menuItemList ) ;
			Assert.NotEmpty ( menuItemList ) ;
			Fixture.MyApp.Resources[ "MyMenuItemList" ] = menuItemList ;
			var found = Fixture.MyApp.FindResource ( "MyMenuItemList" ) ;
			Assert.NotNull ( found ) ;
			var x = string.Join ( ", " , menuItemList.First ( ).Children ) ;
			Logger.Debug ( $"found {found}, {x}" ) ;

			var uri = new Uri ( Fixture.BasePackUri , Resources.MenuResourcesPath ) ;
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
			;
			stack.Push ( entry ) ;

			foreach ( var q in menuResources.Keys )
			{
				var resource = menuResources[ q ] ;
				stack.Push ( myServices.InfoContextFactory ( "key" , q ) ) ;
				Logger.Debug ( $"{q}: {resource}" ) ;
				var prefix = $"Resource[{q}]" ;
				WpfApp1Tests3.DumpHelper.DumpResource (
				                                       stack
				                                     , resource
				                                     , myServices.InfoContextFactory
				                                      ) ;
				stack.Pop ( ) ;
			}
		}

		[ Fact ]
		[ Trait ( "MSBuild" , "Include" ) ]
		[ Attributes.PushContext ( "my pushed context" ) ]
		public void Test2 ( )
		{
			using var c = C ( "using context" ) ;
			DoLog ( "excellent message" ) ;
		}

		[ Fact ]
		[ Trait ( "MSBuild" , "Include" ) ]
		public void Test3 ( )
		{
			using var c = C ( "testxx" ) ;
			using var c2 = C ( "testxx2" ) ;
			DoLog ( "test" ) ;
		}
	}

	public class MyServicesFixture
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public MyServicesFixture ( IMyServices myServices ) { MyServices = myServices ; }

		public IMyServices MyServices { get ; set ; }
	}

	public interface IMyServices

	{
		InfoContext.Factory InfoContextFactory { get ; }
	}
}
