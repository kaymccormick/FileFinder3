using JetBrains.Annotations;
using NLog;
using WpfApp1Tests3.Fixtures;
using Xunit;

namespace WpfApp1Tests3.Collections
{
    [CollectionDefinition( "WpfApp"), UsedImplicitly]

    public class WpfApplication
        : ICollectionFixture < WpfApplicationFixture >,
            ICollectionFixture < ObjectIDFixture >,
            ICollectionFixture < UtilsContainerFixture >
           // , ICollectionFixture <LogFixture>
    {
    }
}
