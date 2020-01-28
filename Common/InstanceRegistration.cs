#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// InstanceRegistration.cs
// 
// 2020-01-25-7:11 PM
// 
// ---
#endregion
using System ;
using AppShared ;


namespace Common
{
	public class InstanceRegistration
	{
		public object Instance { get ; }

		public object ObjectId { get ; }

		public InstanceInfo InstanceInfo { get ; }

		public Type   Type     { get ; }
		public InstanceRegistration (
			object       instance
		  , object       objectId
		  , InstanceInfo instanceInfo
		)
		{
			Instance = instance ;
			ObjectId = objectId ;
			InstanceInfo = instanceInfo ;
			Type     = instance.GetType ( ) ;
		}
	}
}