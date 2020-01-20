using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WpfApp1Tests3
{
    [CollectionDefinition("AutofacContainer")]
    class AutofacContainerDef : ICollectionFixture <ContainerFixture>
    {
    }
}
