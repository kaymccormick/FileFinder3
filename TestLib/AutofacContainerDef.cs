using TestLib.Fixtures ;
using Xunit ;

namespace TestLib
{
	[ CollectionDefinition ( "AutofacContainer" ) ]
	internal class AutofacContainerDef : ICollectionFixture < AppContainerFixture >
	  , ICollectionFixture < LoggingFixture >
	{
	}
}