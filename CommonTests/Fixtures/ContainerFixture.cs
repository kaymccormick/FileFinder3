using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppShared.Modules ;
using Autofac ;
using Common.Logging ;

namespace CommonTests.Fixtures
{
	public class ContainerFixture
	{
		private IContainer _container ;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public ContainerFixture ( ) { _init ( ) ; }

		public IContainer Container => _container ;

		private void _init()
		{
			AppLoggingConfigHelper.EnsureLoggingConfigured();
			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterModule < IdGeneratorModule > ( ) ;
			builder.RegisterType < RandomClass > ( ).As < IRandom > ( ) ;
			
			_container = builder.Build ( ) ;
		}

	}

	public interface IRandom
	{
	}

	public class RandomClass : IRandom
	{
	}
}
