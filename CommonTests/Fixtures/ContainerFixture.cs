using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac ;

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
			ContainerBuilder builder = new ContainerBuilder();
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
