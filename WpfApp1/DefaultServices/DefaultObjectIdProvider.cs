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
using System.ServiceModel.Channels ;
using System.Windows.Documents ;
using AppShared ;
using AppShared.Interfaces ;
using Autofac.Core ;
using NLog ;

namespace WpfApp1.DefaultServices
{
	public class DefaultObjectIdProvider : IObjectIdProvider
	{
		private static Logger Logger = LogManager.GetCurrentClassLogger ( ) ;
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
			if ( byComponent.TryGetValue ( reg.Id , out var compinfo ) )
			{
				return new List < InstanceInfo > ( compinfo.instances) ;
			}

			return Array.Empty < InstanceInfo > ( ) ;
		}

		public int GetInstanceCount ( IComponentRegistration reg )
		{
			return GetInstanceByComponentRegistration ( reg ).Count ;
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
				if ( ! byComponent.TryGetValue ( eComponent.Id , out compreg ) )
				{
					compreg = new CompInfo ( ) ;
					if ( ! byComponent.TryAdd ( eComponent.Id , compreg ) )
					{

					}
				}

				Logger.Debug ( $"Adding {instance} tp reg for {eComponent.Id}" ) ;
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

		public ConcurrentDictionary <Guid, CompInfo> byComponent { get ; set ; } = new ConcurrentDictionary < Guid , CompInfo > ();
	}

	public class CompInfo
	{
		public List < InstanceInfo > instances  = new List < InstanceInfo > ();
	}

	public class UnableToRegisterObjectIdException : Exception
	{
	}
}