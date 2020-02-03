using System.Linq ;
using AppShared ;
using AppShared.Infos ;
using TestLib.Attributes ;
using Xunit ;

namespace WpfApp1Tests3
{
    [LogTestMethod, BeforeAfterLogger]
	public class InfoContextTests
	{
		[ Fact ]
		public void EnumeratorTest ( )
		{
			var name = "test" ;
			var objectContext = "hello" ;
			var x = new InfoContext ( name , objectContext ) ;
			var objects = x.ToList ( ) ;
			Assert.Equal ( 2 ,             objects.Count ) ;
			Assert.Equal ( name ,          objects[ 0 ] ) ;
			Assert.Equal ( objectContext , objects[ 1 ] ) ;
		}
	}
}