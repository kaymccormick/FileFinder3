using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.Globalization ;
using System.Linq ;
using Autofac.Core ;
using Common.Converters ;
using CommonTests.Fixtures ;
using Xunit ;
using Xunit.Abstractions ;

namespace CommonTests
{
	public class TestConverter : IClassFixture < ContainerFixture >
	{
		private readonly ContainerFixture _fixture ;
		private readonly ITestOutputHelper _output ;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TestConverter (ContainerFixture fixture, ITestOutputHelper output)
		{
			_fixture = fixture ;
			_output = output ;
		}

		[ Fact ]
		public void TestConversion1 ( )
		{
			// takes IComponentLifetime
			InstanceRegistrationConverter converter = new InstanceRegistrationConverter();


			IEnumerable < IComponentRegistration > regs = _fixture.Container.ComponentRegistry.Registrations ;
			foreach ( IComponentRegistration reg in regs )
			{
				_output.WriteLine($"{reg.Activator.LimitType.FullName}:\t\t{reg.Id} ");
				foreach ( var regService in reg.Services )
				{
					if ( regService is TypedService t )
					{
						_output.WriteLine ( $"  {t.Description}:\t{t.ServiceType.FullName}" ) ;
					}
					else
					{	_output.WriteLine ( $"  {regService.Description}:\t{regService}" ) ;

					}
				}
			}
			var value = regs.Where(( registration , i ) => true).First ( ) ;
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
