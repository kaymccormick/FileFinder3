#region header
// Kay McCormick (mccor)
// 
// Common
// CommonTests
// TestConverter2.cs
// 
// 2020-01-30-5:54 AM
// 
// ---
#endregion
using System.Collections ;
using System.Collections.Generic ;
using System.Globalization ;
using System.Linq ;
using System.Runtime.Serialization ;
using AppShared.Services ;
using Autofac ;
using Autofac.Core ;
using Common.Converters ;
using CommonTests.Fixtures ;
using Xunit ;
using Xunit.Abstractions ;

namespace CommonTests
{
	public class TestConverter2 : IClassFixture < ContainerFixture >
	{
		private readonly ContainerFixture  _fixture ;
		private readonly ITestOutputHelper _output ;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TestConverter2 (ContainerFixture fixture, ITestOutputHelper output)
		{
			_fixture = fixture ;
			_output  = output ;
		}

		[ Fact ]
		public void TestConversion1 ( )
		{
			// takes IComponentLifetime
			var random = _fixture.Container.Resolve < IRandom > ( ) ;
			RegistrationConverter converter = new RegistrationConverter(
				_fixture.Container, provider : new DefaultObjectIdProvider(new ObjectIDGenerator()));

			IEnumerable < IComponentRegistration > regs = _fixture.Container.ComponentRegistry.Registrations ;
			var value = regs.Where(( registration , i ) => registration.Services.Any(service => service is TypedService t && t.ServiceType == typeof(IRandom))).First ( ) ;
			var result = converter.Convert (
			                                value
			                              , typeof ( IEnumerable )
			                              , null
			                              , CultureInfo.CurrentCulture
			                               ) ;
			Assert.NotNull(result);
			IEnumerable enumerable = ( IEnumerable ) result ;
			Assert.NotEmpty(enumerable);
			foreach ( var o in enumerable )
			{
				_output.WriteLine($"{o}");
			}
		}
	}
}