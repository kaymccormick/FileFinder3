using System ;
using System.Collections ;
using System.Text ;
using System.Collections.Generic ;
using Autofac.Core ;
using WpfApp1.Windows ;
using Xunit ;

namespace WpfApp1Tests3
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	public class UnitTest1
	{
		public UnitTest1 ( )
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[ Fact ]
		public void Tesst1 ( )
		{
			var componentRegistry = new ComponentRegistry ( ) ;
			var componentRegistration = new ComponentRegistration ( ) ;
			componentRegistration.AddChild ( new TypedService ( typeof ( IEnumerable ) ) ) ;
			componentRegistry.AddChild ( componentRegistration ) ;
			var x = new LifetimeScope ( componentRegistry ) ;
		}
	}
}