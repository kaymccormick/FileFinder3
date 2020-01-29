using System ;
using System.Collections ;
using System.Text ;
using System.Collections.Generic ;
using System.Windows.Markup ;
using Autofac.Core ;
using Common ;
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
			IAddChild c = componentRegistration as IAddChild ;
			var typedService = new TypedService ( typeof ( IEnumerable ) ) ;
			//v//ar x = new LifetimeScope ( componentRegistry ) ;
		}
	}
}