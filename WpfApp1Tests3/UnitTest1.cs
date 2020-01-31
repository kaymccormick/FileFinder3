using System.Collections ;
using Autofac.Core ;
using Common ;
using WpfApp1.Windows ;
using Xunit ;

namespace WpfApp1Tests3
{
	/// <summary>
	///     Summary description for UnitTest1
	/// </summary>
	public class UnitTest1
	{
		[ Fact ]
		public void Tesst1 ( )
		{
			var componentRegistry = new ComponentRegistry ( ) ;

			var componentRegistration = new ComponentRegistration ( ) ;
			var c = componentRegistration ;
			var typedService = new TypedService ( typeof ( IEnumerable ) ) ;
			//v//ar x = new LifetimeScope ( componentRegistry ) ;
		}
	}
}