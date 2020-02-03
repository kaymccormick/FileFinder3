using Common.Context ;
using NLog ;
using Xunit ;

namespace WpfApp1Tests3.Attributes
{
	public class PushContextAttributeTests
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;
		[Fact ( )]
		[PushContext("test", "123")]
		public void PushContextAttributeTest () { Logger.Debug ( ( "test" ) ) ; }

		[Fact ( )]
		[PushContext("context1")]
		public void AfterTest ()
		{

		}

		[Fact ( )]
		public void BeforeTest ()
		{
        }
	}
}