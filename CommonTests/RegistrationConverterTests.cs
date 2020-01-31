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
using System.Globalization ;
using System.Linq ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Common.Converters ;
using CommonTests.Fixtures ;
using Xunit ;
using Xunit.Abstractions ;

namespace CommonTests
{
	public class RegistrationConverterTests : IClassFixture < ContainerFixture >
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public RegistrationConverterTests ( ContainerFixture fixture , ITestOutputHelper output )
		{
			_fixture = fixture ;
			_output  = output ;
		}

		private readonly ContainerFixture  _fixture ;
		private readonly ITestOutputHelper _output ;

		[ Fact ]
		public void TestConversion1 ( )
		{
			// takes IComponentLifetime
			var random = _fixture.Container.Resolve < IRandom > ( ) ;
			var objIdProv = _fixture.Container.Resolve < IObjectIdProvider > ( ) ;
			var converter = new RegistrationConverter ( _fixture.Container , objIdProv ) ;

			var regs = _fixture.Container.ComponentRegistry.Registrations ;
			var value = regs.Where (
			                        ( registration , i )
				                        => registration.Services.Any (
				                                                      service
					                                                      => service is TypedService
						                                                         t
					                                                         && t.ServiceType
					                                                         == typeof ( IRandom )
				                                                     )
			                       )
			                .First ( ) ;
			var result = converter.Convert (
			                                value
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
	}
}