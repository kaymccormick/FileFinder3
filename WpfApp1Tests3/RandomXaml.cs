using AppShared ;
using Common.Logging ;
using KayMcCormick.Dev.Test.Metadata ;
using NLog ;
using TestLib.Attributes ;
using Xunit ;

namespace WpfApp1Tests3
{
	[ WpfTestApplication ]
	[ Collection ( "WpfApp" ) ]
	[ BeforeAfterLogger ]
	public class RandomXaml // : IClassFixture <LoggingFixture>
	{
		private static readonly Logger Logger = LoggerProxyHelper.GetCurrentClassLogger ( ) ;

		public LoggerProxyHelper.LogMethod UseLogMethod { get ; set ; }

		[ Fact ]
		public void Test1 ( ) { }
	}
}