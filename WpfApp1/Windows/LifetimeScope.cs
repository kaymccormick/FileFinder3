using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Lifetime ;
using Autofac.Core.Resolving ;

namespace WpfApp1.Windows
{
	public class LifetimeScope : ILifetimeScope
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public LifetimeScope ( object tag , IComponentRegistry componentRegistry = null)
		{
			if ( componentRegistry == null )
			{
				ComponentRegistry = new ComponentRegistry();
			}
			else
			{
				ComponentRegistry = componentRegistry ;
			}

			Tag = tag ;
		}


		/// <summary>
		/// Resolve an instance of the provided registration within the context.
		/// </summary>
		/// <param name="registration">The registration.</param>
		/// <param name="parameters">Parameters for the instance.</param>
		/// <returns>The component instance.</returns>
		/// <exception cref="T:Autofac.Core.Registration.ComponentNotRegisteredException" />
		/// <exception cref="T:Autofac.Core.DependencyResolutionException" />
		public object ResolveComponent ( IComponentRegistration registration , IEnumerable < Parameter > parameters ) { throw new NotImplementedException ( ) ; }

		/// <summary>
		/// Gets the associated services with the components that provide them.
		/// </summary>
		public IComponentRegistry ComponentRegistry { get ; set ; }

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		public void Dispose ( ) { throw new NotImplementedException ( ) ; }

		/// <summary>
		/// Begin a new nested scope. Component instances created via the new scope
		/// will be disposed along with it.
		/// </summary>
		/// <returns>A new lifetime scope.</returns>
		public ILifetimeScope BeginLifetimeScope ( ) { throw new NotImplementedException ( ) ; }

		/// <summary>
		/// Begin a new nested scope. Component instances created via the new scope
		/// will be disposed along with it.
		/// </summary>
		/// <param name="tag">The tag applied to the <see cref="T:Autofac.ILifetimeScope" />.</param>
		/// <returns>A new lifetime scope.</returns>
		public ILifetimeScope BeginLifetimeScope ( object tag ) { throw new NotImplementedException ( ) ; }

		/// <summary>
		/// Begin a new nested scope, with additional components available to it.
		/// Component instances created via the new scope
		/// will be disposed along with it.
		/// </summary>
		/// <remarks>
		/// The components registered in the sub-scope will be treated as though they were
		/// registered in the root scope, i.e., SingleInstance() components will live as long
		/// as the root scope.
		/// </remarks>
		/// <param name="configurationAction">Action on a <see cref="T:Autofac.ContainerBuilder" />
		/// that adds component registrations visible only in the new scope.</param>
		/// <returns>A new lifetime scope.</returns>
		public ILifetimeScope BeginLifetimeScope ( Action < ContainerBuilder > configurationAction ) { throw new NotImplementedException ( ) ; }

		/// <summary>
		/// Begin a new nested scope, with additional components available to it.
		/// Component instances created via the new scope
		/// will be disposed along with it.
		/// </summary>
		/// <remarks>
		/// The components registered in the sub-scope will be treated as though they were
		/// registered in the root scope, i.e., SingleInstance() components will live as long
		/// as the root scope.
		/// </remarks>
		/// <param name="tag">The tag applied to the <see cref="T:Autofac.ILifetimeScope" />.</param>
		/// <param name="configurationAction">Action on a <see cref="T:Autofac.ContainerBuilder" />
		/// that adds component registrations visible only in the new scope.</param>
		/// <returns>A new lifetime scope.</returns>
		public ILifetimeScope BeginLifetimeScope ( object tag , Action < ContainerBuilder > configurationAction ) { throw new NotImplementedException ( ) ; }

		/// <summary>
		/// Gets the disposer associated with this <see cref="T:Autofac.ILifetimeScope" />.
		/// Component instances can be associated with it manually if required.
		/// </summary>
		/// <remarks>Typical usage does not require interaction with this member- it
		/// is used when extending the container.</remarks>
		public IDisposer Disposer { get ; set ; }

		/// <summary>
		/// Gets the tag applied to the <see cref="T:Autofac.ILifetimeScope" />.
		/// </summary>
		/// <remarks>Tags allow a level in the lifetime hierarchy to be identified.
		/// In most applications, tags are not necessary.</remarks>
		/// <seealso cref="M:Autofac.Builder.IRegistrationBuilder`3.InstancePerMatchingLifetimeScope(System.Object[])" />
		public object Tag { get ; set ; }

		/// <summary>
		/// Fired when a new scope based on the current scope is beginning.
		/// </summary>
		public event EventHandler < LifetimeScopeBeginningEventArgs > ChildLifetimeScopeBeginning ;

		/// <summary>Fired when this scope is ending.</summary>
		public event EventHandler < LifetimeScopeEndingEventArgs > CurrentScopeEnding ;

		/// <summary>
		/// Fired when a resolve operation is beginning in this scope.
		/// </summary>
		public event EventHandler < ResolveOperationBeginningEventArgs > ResolveOperationBeginning ;
	}
}
