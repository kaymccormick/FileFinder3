using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Net ;
using System.Text ;
using System.Threading.Tasks ;
using Makaretu.Dns ;
using Xunit ;

namespace WpfApp1Tests3
{
	public class ZeroconfTests
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public ZeroconfTests (  ) { }

		//[ Fact ]
		public void AdvertiseTest (  )
		{
			var zone =  "_log4j_xml_udp_appender.local." ;
			var sd = new ServiceDiscovery ( ) ;
			var domainRoot = DomainName.Root ;
			Assert.True ( IPAddress.TryParse ( "10.25.0.102" , out var ipAddress ) ) ;
			var	service	= new ServiceProfile (
			                                  new DomainName ( "x" )
			                                , new DomainName ( zone )
			                                , 4444
			                                , new IPAddress[] { ipAddress }
			                                 ) ;


			sd.Advertise ( service ) ;
			Console.ReadKey ( ) ;
		}
	}
}