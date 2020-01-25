#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// IObjectIdProvider.cs
// 
// 2020-01-25-12:54 AM
// 
// ---
#endregion
using System.Collections ;

namespace WpfApp1.Interfaces
{
	public interface IObjectIdProvider
	{
		IEnumerable GetObjectInstances ( ) ;
		object ProvideObjectInstanceIdentifier ( object instance) ;
	}
}