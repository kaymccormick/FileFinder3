using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog ;

namespace AppSharedTests
{
	public class Class1
	{
		[ Fact ]
		public void Test1 ( ) { LogManager.GetCurrentClassLogger ( ).Info ( "Hello" ) ; }

	}
}
