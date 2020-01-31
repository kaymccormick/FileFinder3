using System.Collections.Generic ;
using AppShared ;
using Autofac ;
using Common ;

namespace WpfApp1
{
	public class DesignData
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public DesignData ( )
		{
			var containerBuilder = new ContainerBuilder ( ) ;
			containerBuilder.RegisterModule < MenuModule > ( ) ;
			LifetimeScope = containerBuilder.Build ( ).BeginLifetimeScope ( "Design scope" ) ;
		}

		public ILifetimeScope LifetimeScope { get ; }

		public static List < InstanceInfo > InstanceList { get ; } = new List < InstanceInfo > ( ) ;

		public static InstanceInfo InstanceInfo { get ; } = new InstanceInfo ( ) ;
	}
}