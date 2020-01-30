using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTests.Fixtures ;
using Xunit ;

namespace CommonTests
{
    public class Class1 : IClassFixture <ContainerFixture>
    {
	    private ContainerFixture _containerFixture ;

	    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
	    public Class1 ( ContainerFixture containerFixture )
	    {
		    _containerFixture = containerFixture ;
	    }

	    [ Fact ]
	    public void Test1 ( )
	    {
			Assert.NotNull(_containerFixture.Container);
			Assert.NotNull(_containerFixture.Container.ComponentRegistry);
			Assert.True(_containerFixture.Container.ComponentRegistry.HasLocalComponents);
	    }
    }
}
