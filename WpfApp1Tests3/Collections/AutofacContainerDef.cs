using WpfApp1Tests3.Fixtures;
using Xunit;

namespace WpfApp1Tests3.Collections
{
    [CollectionDefinition("AutofacContainer")]
    class AutofacContainerDef : ICollectionFixture <ContainerFixture>
    {
    }
}
