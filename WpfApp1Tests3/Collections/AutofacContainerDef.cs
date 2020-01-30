using TestLib ;
using TestLib.Fixtures ;
using Xunit;

namespace WpfApp1Tests3.Collections
{
    [CollectionDefinition("AutofacContainer")]
    class AutofacContainerDef : ICollectionFixture <ContainerFixture>, ICollectionFixture <LoggingFixture>
    {
    }
}
