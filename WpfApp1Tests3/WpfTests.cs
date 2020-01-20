using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WpfApp1Tests3
{
    [Collection("WpfApp")]
    public class WpfTests
    {
        public GenericApplicationFixture fixture;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WpfTests(
            GenericApplicationFixture fixture
        )
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Test1()
        {
            Assert.True( false );
        }

    }
}
