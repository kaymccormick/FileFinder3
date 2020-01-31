using System ;
using System.Net ;
using Makaretu.Dns ;
using Xunit ;

namespace WpfApp1Tests3
{
	public class ZeroconfTests
	{
		//[ Fact ]
		public void AdvertiseTest ( )
		{
			var zone = "_log4j_xml_udp_appender.local." ;
			var sd = new ServiceDiscovery ( ) ;
			var domainRoot = DomainName.Root ;
			Assert.True ( IPAddress.TryParse ( "10.25.0.102" , out var ipAddress ) ) ;
			var service = new ServiceProfile (
			                                  new DomainName ( "x" )
			                                , new DomainName ( zone )
			                                , 4444
			                                , new[] { ipAddress }
			                                 ) ;


			sd.Advertise ( service ) ;
			Console.ReadKey ( ) ;
		}
	}
}