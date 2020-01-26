using Xunit;
using WpfApp1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog ;

namespace WpfApp1.AttributesTests3
{
	public class PushContextAttributeTests
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;
		[Fact ( )]
		[PushContext("test", "123")]
		public void PushContextAttributeTest () { Logger.Debug ( ( "test" ) ) ; }

		[Fact ( )]
		public void AfterTest ()
		{
			Assert.True ( false , "This test needs an implementation" );
		}

		[Fact ( )]
		public void BeforeTest ()
		{
			Assert.True ( false , "This test needs an implementation" );
		}
	}
}