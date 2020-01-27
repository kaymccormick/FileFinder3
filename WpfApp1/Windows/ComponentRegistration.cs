﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// ComponentRegistration.cs
// 
// 2020-01-26-9:13 AM
// 
// ---
#endregion
using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Linq.Dynamic ;
using System.Management ;
using System.Runtime.Serialization ;
using System.Windows.Markup ;
using AppShared ;
using Autofac ;
using Autofac.Core ;

namespace WpfApp1.Windows
{
	[ ContentProperty ( "ServicesList" ) ]
	public class ComponentRegistration : IComponentRegistration , IAddChild
	{
		private IEnumerable < Service > _services ;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public ComponentRegistration (
			Guid                            id
		  , IInstanceActivator              activator
		  , IComponentLifetime              lifetime
		  , InstanceSharing                 sharing
		  , InstanceOwnership               ownership
		  , IEnumerable < Service >         services
		  , IDictionary < string , object > metadata
		  , IComponentRegistration          target
		)
		{
			Id        = id ;
			Activator = activator ;
			Lifetime  = lifetime ;
			Sharing   = sharing ;
			Ownership = ownership ;
			Services  = services ;
			Metadata  = metadata ;
			Target    = target ;
		}

		public List < object > ServicesList { get ; set ; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public ComponentRegistration (
			IComponentLifetime     lifetime
		  , InstanceSharing        sharing
		  , InstanceOwnership      ownership
		  , IComponentRegistration target
		)
		{
			Id        = Guid.NewGuid ( ) ;
			Lifetime  = lifetime ;
			Sharing   = sharing ;
			Ownership = ownership ;
			Target    = target ;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		/// [ContentP
		
		public ComponentRegistration ( ) 
		{
			Id       = Guid.NewGuid ( ) ;
			Services = new List < Service > ( ) ;
			Metadata = new Dictionary < string , object > ( ) ;
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		public void Dispose ( ) { throw new NotImplementedException ( ) ; }

		/// <summary>Called by the container when an instance is required.</summary>
		/// <param name="context">The context in which the instance will be activated.</param>
		/// <param name="parameters">Parameters for activation. These may be modified by the event handler.</param>
		public void RaisePreparing (
			IComponentContext             context
		  , ref IEnumerable < Parameter > parameters
		)
		{
			throw new NotImplementedException ( ) ;
		}

		/// <summary>
		/// Called by the container once an instance has been constructed.
		/// </summary>
		/// <param name="context">The context in which the instance was activated.</param>
		/// <param name="parameters">The parameters supplied to the activator.</param>
		/// <param name="instance">The instance.</param>
		public void RaiseActivating (
			IComponentContext         context
		  , IEnumerable < Parameter > parameters
		  , ref object                instance
		)
		{
			throw new NotImplementedException ( ) ;
		}

		/// <summary>
		/// Called by the container once an instance has been fully constructed, including
		/// any requested objects that depend on the instance.
		/// </summary>
		/// <param name="context">The context in which the instance was activated.</param>
		/// <param name="parameters">The parameters supplied to the activator.</param>
		/// <param name="instance">The instance.</param>
		public void RaiseActivated (
			IComponentContext         context
		  , IEnumerable < Parameter > parameters
		  , object                    instance
		)
		{
			throw new NotImplementedException ( ) ;
		}

		/// <summary>
		/// Gets a unique identifier for this component (shared in all sub-contexts.)
		/// This value also appears in Services.
		/// </summary>
		public Guid Id { get ; set ; }

		/// <summary>Gets the activator used to create instances.</summary>
		public IInstanceActivator Activator { get ; set ; }

		/// <summary>Gets the lifetime associated with the component.</summary>
		public IComponentLifetime Lifetime { get ; set ; }

		/// <summary>
		/// Gets a value indicating whether the component instances are shared or not.
		/// </summary>
		public InstanceSharing Sharing { get ; set ; }

		/// <summary>
		/// Gets a value indicating whether the instances of the component should be disposed by the container.
		/// </summary>
		public InstanceOwnership Ownership { get ; set ; }

		/// <summary>Gets the services provided by the component.</summary>
		public IEnumerable < Service > Services
		{
			get
			{
				if ( ServicesList != null )
				{
					return ServicesList.AsQueryable ( )
					                   .Select ( s => s as Service )
					                   .Where ( s => s != null ) ;
				}

				return _services ;
			}
			protected set => _services = value ;
		}


		/// <summary>Gets additional data associated with the component.</summary>
		public IDictionary < string , object > Metadata { get ; set ; }

		/// <summary>
		/// Gets the component registration upon which this registration is based.
		/// </summary>
		public IComponentRegistration Target { get ; set ; }

		public IList < InstanceInfo > Instances { get ; set ; } = new List < InstanceInfo > ( ) ;

		/// <summary>
		/// Fired when a new instance is required, prior to activation.
		/// Can be used to provide Autofac with additional parameters, used during activation.
		/// </summary>
		public event EventHandler < PreparingEventArgs > Preparing ;

		/// <summary>
		/// Fired when a new instance is being activated. The instance can be
		/// wrapped or switched at this time by setting the Instance property in
		/// the provided event arguments.
		/// </summary>
		public event EventHandler < ActivatingEventArgs < object > > Activating ;

		/// <summary>
		/// Fired when the activation process for a new instance is complete.
		/// </summary>
		public event EventHandler < ActivatedEventArgs < object > > Activated ;

		/// <summary>Adds a child object.</summary>
		/// <param name="value">The child object to add.</param>
		public void AddChild ( object value ) { ServicesList.Add ( value ) ; }

		/// <summary>Adds the text content of a node to the object.</summary>
		/// <param name="text">The text to add to the object.</param>
		public void AddText ( string text ) { AddChild ( text ) ; }

	}
}