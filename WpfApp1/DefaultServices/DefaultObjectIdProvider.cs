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
using System.Collections.Generic ;
using System.Runtime.Serialization ;
using System.Windows.Documents ;
using AppShared ;
using AppShared.Interfaces ;
using Autofac.Core ;

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

		public IList<InstanceInfo>  GetInstanceByComponentRegistration ( IComponentRegistration reg )
		{
			if(reg == null)
			{
				return Array.Empty < InstanceInfo > ( ) ;
			}
			if ( byComponent.TryGetValue ( reg , out var compinfo ) )
			{
				return compinfo.instances ;
			}

			return Array.Empty < InstanceInfo > ( ) ;
		}

		public IEnumerable GetObjectInstances ( ) { return registry.Keys ; }

		public object ProvideObjectInstanceIdentifier (
			object                    instance
		  , IComponentRegistration    eComponent
		  , IEnumerable < Parameter > eParameters
		)
		{

			var id_ = Generator.GetId(instance, out var newFlag) ;
			if ( newFlag )
			{
				CompInfo compreg = null ;
				if ( ! byComponent.TryGetValue ( eComponent , out compreg ) )
				{
					compreg = new CompInfo ( ) ;
					if ( ! byComponent.TryAdd ( eComponent , compreg ) )
					{

					}
				}

				compreg.instances.Add (
				                       new InstanceInfo ( )
				                       {
					                       Instance = instance , Parameters = eParameters ,
				                       }
				                      ) ;
				if ( ! registry.TryAdd ( instance , id_ ) )
				{
					throw new UnableToRegisterObjectIdException ( ) ;
				}
			}

			return id_ ;

		}

		public ConcurrentDictionary <object, CompInfo> byComponent { get ; set ; } = new ConcurrentDictionary < object , CompInfo > ();
	}

	public class CompInfo
	{
		public List < InstanceInfo > instances  = new List < InstanceInfo > ();
	}

	public class UnableToRegisterObjectIdException : Exception
	{
	}
}