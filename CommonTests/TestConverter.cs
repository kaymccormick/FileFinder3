using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Globalization ;
using System.Linq ;
using AppShared ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Common ;
using Common.Converters ;
using CommonTests.Fixtures ;
using Xunit ;
using Xunit.Abstractions ;
using Container = Autofac.Core.Container ;

namespace CommonTests
{
	public class TestConverter : IClassFixture < ContainerFixture >
	{
		private readonly ContainerFixture  _fixture ;
		private readonly ITestOutputHelper _output ;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TestConverter ( ContainerFixture fixture , ITestOutputHelper output )
		{
			_fixture = fixture ;
			_output  = output ;
		}

		[ WpfFact ]
		public void TestConversion1 ( )
		{
			// takes IComponentLifetime

			var random = _fixture.Container.Resolve < Lazy<IRandom>> ( ) ;
			var objIdProv = _fixture.Container.Resolve < IObjectIdProvider > ( ) ;

			var instConv =
				new RegistrationConverter (_fixture.Container, objIdProv ) ; // TypeDescriptor.GetConverter ( typeof (IComponentRegistration) ) ;
			Assert.NotNull ( instConv ) ;
			_output.WriteLine ( $"{instConv}" ) ;

			var converter = new InstanceRegistrationConverter ( ) ;

			var regs = _fixture.Container.ComponentRegistry.Registrations ;
			NewMethod ( regs ) ;

			var value = regs.Where (
			                        ( registration , i )
				                        => registration.Services.Any (
				                                                      service
					                                                      => service is TypedService
						                                                         t
					                                                         && t.ServiceType
					                                                         == random.GetType() )
				                                                     
			                       )
			                .First ( ) ;

			var myVal = instConv.Convert (
			                              value
			                            , null
			                            , _fixture.Container
			                            , CultureInfo.CurrentCulture
			                             ) ;

			Assert.NotNull ( myVal ) ;
			Assert.IsAssignableFrom < IList < InstanceRegistration > > ( myVal ) ;
			IList < InstanceRegistration> list = ( IList < InstanceRegistration> ) myVal ;
			Assert.Collection ( list , info => {
				                   Assert.NotNull ( info.Instance ) ;
			                   }
			                  ) ;
			
			var result = converter.Convert (
			                                list[0]
			                              , typeof ( IEnumerable )
			                              , null
			                              , CultureInfo.CurrentCulture
			                               ) ;
			Assert.NotNull ( result ) ;
			var enumerable = ( IEnumerable ) result ;
			Assert.NotEmpty ( enumerable ) ;
			foreach ( var o in enumerable )
			{
				_output.WriteLine ( $"{o}" ) ;
			}
		}

		private void NewMethod ( IEnumerable < IComponentRegistration > regs )
		{
			foreach ( var reg in regs )
			{
				_output.WriteLine ( $"{reg.Activator.LimitType.FullName}:\t\t{reg.Id} " ) ;
				foreach ( var regService in reg.Services )
				{
					if ( regService is TypedService t )
					{
						_output.WriteLine ( $"  {t.Description}:\t{t.ServiceType.FullName}" ) ;
					}
					else
					{
						_output.WriteLine ( $"  {regService.Description}:\t{regService}" ) ;
					}
				}
			}
		}
	}
}