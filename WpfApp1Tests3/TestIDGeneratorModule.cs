using AppShared.Interfaces ;
using AppShared.Modules ;
using Autofac ;
using TestLib.Attributes ;
using Xunit ;
using Xunit.Abstractions ;

namespace WpfApp1Tests3
{
    [LogTestMethod, BeforeAfterLogger]
	public class TestIDGeneratorModule
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public TestIDGeneratorModule ( ITestOutputHelper output ) { _output = output ; }

		private readonly ITestOutputHelper _output ;

		private static IContainer Setup ( )
		{
			var b = new ContainerBuilder ( ) ;
			b.RegisterModule ( new IdGeneratorModule ( ) ) ;
			b.RegisterType < MyService > ( ).AsSelf ( ) ;
			var container = b.Build ( ) ;
			return container ;
		}

		[ Fact ]
		public void Test1 ( )
		{
			var container = Setup ( ) ;
			var myService = container.Resolve < MyService > ( ) ;
			_output.WriteLine ( myService.ToString ( ) ) ;

			var idProvider = container.Resolve < IObjectIdProvider > ( ) ;
			foreach ( var q in idProvider.GetObjectInstances ( ) )
			{
				var id = idProvider.ProvideObjectInstanceIdentifier ( q , null , null ) ;
				_output.WriteLine ( $"{q} = {id}" ) ;
			}
		}
	}
}