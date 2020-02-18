using Xunit;
using WpfApp1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Context ;
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