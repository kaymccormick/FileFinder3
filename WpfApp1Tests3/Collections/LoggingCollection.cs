using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib ;
using WpfApp1Tests3.Fixtures ;
using Xunit ;

namespace WpfApp1Tests3.Collections
{
	[CollectionDefinition( "Logging")]
	class LoggingCollection : ICollectionFixture <LoggingFixture>
	{
	}
}
