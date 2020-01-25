#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// DefaultObjectIdProvider.cs
// 
// 2020-01-25-12:56 AM
// 
// ---
#endregion
using System ;
using System.Collections ;
using System.Collections.Concurrent ;
using System.Runtime.Serialization ;
using WpfApp1.Interfaces ;

namespace WpfApp1.DefaultServices
{
	public class DefaultObjectIdProvider : IObjectIdProvider
	{
		public ConcurrentDictionary < object , long > registry ;
		public ConcurrentDictionary < long , object > registryById ;
		public ObjectIDGenerator Generator { get ; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public DefaultObjectIdProvider (ObjectIDGenerator generator )
		{
			Generator = generator ;
			registry = new ConcurrentDictionary < object , long > ();
		}

		public IEnumerable GetObjectInstances ( ) { return registry.Keys ; }

		public object ProvideObjectInstanceIdentifier ( object instance )
		{
			var id_ = Generator.GetId(instance, out var newFlag) ;
			if ( newFlag )
			{
				if ( ! registry.TryAdd ( instance , id_ ) )
				{
					throw new UnableToRegisterObjectIdException ( ) ;
				}
			}

			return id_ ;

		}
	}

	public class UnableToRegisterObjectIdException : Exception
	{
	}
}