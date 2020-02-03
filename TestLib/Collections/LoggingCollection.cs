using TestLib.Fixtures ;
using Xunit ;

namespace TestLib.Collections
{
	[ CollectionDefinition ( "Logging" ) ]
	internal class LoggingCollection : ICollectionFixture < LoggingFixture >
	{
	}
}