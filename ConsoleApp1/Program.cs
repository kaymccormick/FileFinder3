using System;
using System.IO;
using System.Net ;
using System.Reactive.Subjects;
using System.Threading ;
using System.Threading.Tasks;
using FileFinder3;
using Makaretu.Dns ;
using NLog;
using WpfApp1.Util ;

namespace ConsoleApp1
{
	internal class Program
	{
		private static readonly Logger Logger =
			LogManager.GetCurrentClassLogger();

		private static async Task Main ( )
		{
			
			var sd = new ServiceDiscovery ( ) ;

			var zone =  "_log4j_xml_udp" + "_appender.local." ;
			//var sd = new ServiceDiscovery ( ) ;
			var domainRoot = DomainName.Root ;
			if ( !IPAddress.TryParse ( "10.25.0.102" , out var ipAddress ) )
			{
				return ;
			}
		
			var id = "_log4j_xml_udp_appender.local.local" ;
		
			var	service	= new ServiceProfile (
			                                  new DomainName ( "x" )
			                                , new DomainName ( zone )
			                                , 4444
			                                , new IPAddress[] { ipAddress }
			                                 ) ;
		
			//
			sd.Advertise ( service ) ;
			sd.ServiceDiscovered += ( s , serviceName ) => {
				Console.WriteLine ( $"{serviceName}" ) ;
				// Do something };
			} ;
			await Task.Delay ( ( 10000 ) ) ;
		}

		
			//Console.ReadKey ( ) ;
		//
		// }
		

		private static async Task Main1()
		{
			var subject = new ReplaySubject < FileSystemInfo >();
			subject.Subscribe( info => {Logger.Debug( $"got {info}" );} );
			var task = Task.Run(
								() => {
									var finder = new FileFinderImpl3
												 {
													 FindDir
														 = @"c:\temp",
													 Observer = subject
												 };
									finder.FindFiles();
								}
							   );
			await task;
		}
	}
}