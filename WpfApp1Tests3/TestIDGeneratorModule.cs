using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Runtime.Remoting.Contexts ;
using System.Text ;
using System.Threading.Tasks ;
using AppShared.Interfaces ;
using Autofac ;
using WpfApp1 ;
using Xunit ;
using Xunit.Abstractions ;

namespace WpfApp1Tests3
{
	public class TestIDGeneratorModule
	{
		private readonly ITestOutputHelper _output ;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TestIDGeneratorModule ( ITestOutputHelper output ) { _output = output ; }

		[ Fact ]
		public void Test1 ( )
		{
			var container = Setup ( ) ;
			var myService = container.Resolve < MyService > ( ) ;
			_output.WriteLine ( myService.ToString ( ) ) ;

			var idProvider = container.Resolve<IObjectIdProvider>();
			foreach(var q in idProvider.GetObjectInstances())
			{
				var id = idProvider.ProvideObjectInstanceIdentifier ( q, null, null) ;
				_output.WriteLine ( $"{q} = {id}" ) ;
			}
		}

		private static IContainer Setup ( )
		{
			ContainerBuilder b = new ContainerBuilder ( ) ;
			b.RegisterModule ( new IdGeneratorModule ( ) ) ;
			b.RegisterType < MyService > ( ).AsSelf ( ) ;
			var container = b.Build ( ) ;
			return container ;
		}
	}
}