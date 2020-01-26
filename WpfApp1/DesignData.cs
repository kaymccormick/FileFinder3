using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Text ;
using System.Threading.Tasks ;
using AppShared ;
using Autofac ;
using WpfApp1.Util ;

namespace WpfApp1
{
	public class DesignData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public DesignData ( )
		{
			var containerBuilder = ( new ContainerBuilder ( ) ) ;
			containerBuilder.RegisterModule < MenuModule > ( ) ;
			LifetimeScope = containerBuilder.Build ( ).BeginLifetimeScope ( "Design scope" ) ;
		}

		public  ILifetimeScope LifetimeScope { get ; }

		public static List < InstanceInfo > InstanceList { get ; } = new List < InstanceInfo > ( ) ;

		public static InstanceInfo InstanceInfo { get ; } = new InstanceInfo ( ) ;
	}
}

