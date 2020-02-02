#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// ComponentRegistry.cs
// 
// 2020-01-26-9:09 AM
// 
// ---
#endregion
using System ;
using System.Collections.Generic ;
using System.Windows.Markup ;
using Autofac.Core ;

namespace WpfApp1.Windows
{
	[ ContentProperty ( "RegistrationsList" ) ]
	public class ComponentRegistry : IComponentRegistry , IAddChild
	{
		private IEnumerable < IComponentRegistration > _registrations ;

		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public ComponentRegistry ( )
		{
			RegistrationsList = new List < IComponentRegistration > ( ) ;
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public ComponentRegistry (
			IEnumerable < IComponentRegistration > registrations
		  , IDictionary < string , object >        properties
		  , IEnumerable < IRegistrationSource >    sources
		  , bool                                   hasLocalComponents
		)
		{
			Properties         = properties ;
			Registrations      = registrations ;
			Sources            = sources ;
			HasLocalComponents = hasLocalComponents ;
		}


		public List < IComponentRegistration > RegistrationsList { get ; set ; }

		/// <summary>Adds a child object.</summary>
		/// <param name="value">The child object to add.</param>
		public void AddChild ( object value )
		{
			RegistrationsList.Add ( value as IComponentRegistration ) ;
		}

		/// <summary>Adds the text content of a node to the object.</summary>
		/// <param name="text">The text to add to the object.</param>
		public void AddText ( string text ) { }

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing,
		///     or resetting unmanaged resources.
		/// </summary>
		public void Dispose ( ) { throw new NotImplementedException ( ) ; }

		/// <summary>
		///     Attempts to find a default registration for the specified service.
		/// </summary>
		/// <param name="service">The service to look up.</param>
		/// <param name="registration">The default registration for the service.</param>
		/// <returns>True if a registration exists.</returns>
		public bool TryGetRegistration ( Service service , out IComponentRegistration registration )
		{
			throw new NotImplementedException ( ) ;
		}

		/// <summary>
		///     Determines whether the specified service is registered.
		/// </summary>
		/// <param name="service">The service to test.</param>
		/// <returns>True if the service is registered.</returns>
		public bool IsRegistered ( Service service ) { throw new NotImplementedException ( ) ; }

		/// <summary>Register a component.</summary>
		/// <param name="registration">The component registration.</param>
		public void Register ( IComponentRegistration registration )
		{
			throw new NotImplementedException ( ) ;
		}

		/// <summary>Register a component.</summary>
		/// <param name="registration">The component registration.</param>
		/// <param name="preserveDefaults">
		///     If true, existing defaults for the services provided by the
		///     component will not be changed.
		/// </param>
		public void Register ( IComponentRegistration registration , bool preserveDefaults )
		{
			throw new NotImplementedException ( ) ;
		}

		/// <summary>
		///     Selects from the available registrations after ensuring that any
		///     dynamic registration sources that may provide <paramref name="service" />
		///     have been invoked.
		/// </summary>
		/// <param name="service">The service for which registrations are sought.</param>
		/// <returns>Registrations supporting <paramref name="service" />.</returns>
		public IEnumerable < IComponentRegistration > RegistrationsFor ( Service service )
		{
			throw new NotImplementedException ( ) ;
		}

        /// <summary>
        /// Selects all available decorator registrations that can be applied to the specified service.
        /// </summary>
        /// <param name="service">The service for which decorator registrations are sought.</param>
        /// <returns>Decorator registrations applicable to <paramref name="service" />.</returns>
        public IReadOnlyList < IComponentRegistration > DecoratorsFor ( IServiceWithType service ) { throw new NotImplementedException ( ) ; }

        /// <summary>
		///     Selects all available decorator registrations that can be applied to the
		///     specified registration.
		/// </summary>
		/// <param name="registration">
		///     The registration for which decorator registrations
		///     are sought.
		/// </param>
		/// <returns>
		///     Decorator registrations applicable to <paramref name="registration" />
		///     .
		/// </returns>
		public IEnumerable < IComponentRegistration > DecoratorsFor (
			IComponentRegistration registration
		)
		{
			throw new NotImplementedException ( ) ;
		}

		/// <summary>
		///     Add a registration source that will provide registrations on-the-fly.
		/// </summary>
		/// <param name="source">The source to register.</param>
		public void AddRegistrationSource ( IRegistrationSource source )
		{
			throw new NotImplementedException ( ) ;
		}

		/// <summary>
		///     Gets the set of properties used during component registration.
		/// </summary>
		/// <value>
		///     An <see cref="T:System.Collections.Generic.IDictionary`2" /> that can be
		///     used to share
		///     context across registrations.
		/// </value>
		public IDictionary < string , object > Properties { get ; set ; }

		/// <summary>Gets the set of registered components.</summary>
		public IEnumerable < IComponentRegistration > Registrations
		{
			get
			{
				if ( RegistrationsList != null )
				{
					return RegistrationsList ;
				}

				return _registrations ;
			}
			private set => _registrations = value ;
		}

		/// <summary>
		///     Gets the registration sources that are used by the registry.
		/// </summary>
		public IEnumerable < IRegistrationSource > Sources { get ; set ; }

		/// <summary>
		///     Gets a value indicating whether the registry contains its own components.
		///     True if the registry contains its own components; false if it is forwarding
		///     registrations from another external registry.
		/// </summary>
		/// <remarks>
		///     This property is used when walking up the scope tree looking for
		///     registrations for a new customised scope.
		/// </remarks>
		public bool HasLocalComponents { get ; set ; }

		/// <summary>
		///     Fired whenever a component is registered - either explicitly or via a
		///     <see cref="T:Autofac.Core.IRegistrationSource" />.
		/// </summary>
		public event EventHandler < ComponentRegisteredEventArgs > Registered ;

		/// <summary>
		///     Fired when an <see cref="T:Autofac.Core.IRegistrationSource" /> is added to
		///     the registry.
		/// </summary>
		public event EventHandler < RegistrationSourceAddedEventArgs > RegistrationSourceAdded ;
	}
}