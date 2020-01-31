using TestLib ;
using Xunit ;

namespace WpfApp1Tests3.Collections
{
	[ CollectionDefinition ( "Logging" ) ]
	internal class LoggingCollection : ICollectionFixture < LoggingFixture >
	{
	}
}