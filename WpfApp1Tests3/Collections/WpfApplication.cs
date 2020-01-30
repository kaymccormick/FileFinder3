using JetBrains.Annotations;
using NLog;
using TestLib ;
using WpfApp1Tests3.Fixtures;
using Xunit;

namespace WpfApp1Tests3.Collections
{
    [CollectionDefinition( "WpfApp"), UsedImplicitly]

    public class WpfApplication
        : ICollectionFixture < WpfApplicationFixture >,
            ICollectionFixture < ObjectIDFixture >,
            ICollectionFixture < UtilsContainerFixture >, ICollectionFixture <LoggingFixture>

           // , ICollectionFixture <LogFixture>
    {
	    
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WpfApplication()
	    {
            
	    }
    }
}
