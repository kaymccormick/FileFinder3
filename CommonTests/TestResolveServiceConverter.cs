#region header
// Kay McCormick (mccor)
// 
// Common
// CommonTests
// TestResolveServiceConverter.cs
// 
// 2020-01-30-7:10 AM
// 
// ---
#endregion
using System ;
using System.Globalization ;
using AppShared ;
using AppShared.Interfaces ;
using Autofac ;
using Common.Converters ;
using CommonTests.Fixtures ;
using Xunit ;
using Xunit.Abstractions ;

namespace CommonTests
{
	public class TestResolveServiceConverter : IClassFixture < ContainerFixture >
	{
		private readonly ContainerFixture  _fixture ;
		private readonly ITestOutputHelper _output ;

		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public TestResolveServiceConverter ( ContainerFixture fixture , ITestOutputHelper output )
		{
			_fixture = fixture ;
			_output  = output ;
		}

		[ WpfFact ]
		public void TestConversion1 ( )
		{
			// takes IComponentLifetime
			var svc = new ResolveService ( ) ;
			svc.ServiceType = typeof ( Lazy < IRandom > ) ;

			_fixture.Container.Resolve < Lazy < IRandom > > ( ) ;
			var objIdProv = _fixture.Container.Resolve < IObjectIdProvider > ( ) ;

			var resolveConv = new ResolveServiceConverter ( ) ;
			Assert.NotNull ( resolveConv ) ;

			var value = resolveConv.Convert (
			                                 svc
			                               , null
			                               , _fixture.Container
			                               , CultureInfo.CurrentCulture
			                                ) ;
			Assert.NotNull ( value ) ;
			Assert.IsAssignableFrom < Lazy < IRandom > > ( value ) ;
			var lazy = ( Lazy < IRandom > ) value ;
			_output.WriteLine ( $"IsValueCreated = {lazy.IsValueCreated}" ) ;
			_output.WriteLine ( $"Value = {lazy.Value}" ) ;
		}
	}
}