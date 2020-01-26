#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// MenuModule.cs
// 
// 2020-01-25-2:03 PM
// 
// ---
#endregion
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Autofac.Extras.DynamicProxy ;
using WpfApp1.Logging ;
using WpfApp1.Menus ;

namespace WpfApp1.Util
{
	public class MenuModule : Module
	{
		/// <summary>Override to add registrations to the container.</summary>
		/// <remarks>
		/// Note that the ContainerBuilder parameter is unique to this module.
		/// </remarks>
		/// <param name="builder">The builder through which components can be
		/// registered.</param>
		protected override void Load ( ContainerBuilder builder )
		{
			builder.RegisterAssemblyTypes ( ThisAssembly )
			       .Where ( predicate : t => typeof ( ITopLevelMenu ).IsAssignableFrom ( c : t ) )
		
		   .As < ITopLevelMenu > ( ) ;
			#region Menu Item Lists
			builder.RegisterType < MenuItemList > ( )
			       .AsImplementedInterfaces ( )
			       .WithMetadata < ResourceMetadata
			        > ( configurationAction : m => m.For ( propertyAccessor : rn => rn.ResourceName , value : "MenuItemList" ) )
			       .PreserveExistingDefaults ( )
			       .EnableInterfaceInterceptors ( )
			       .InterceptedBy ( typeof ( LoggingInterceptor ) ) ;
			builder.RegisterType < XMenuItem > ( )
			       .AsImplementedInterfaces ( )
			       .PreserveExistingDefaults ( )
			       .EnableInterfaceInterceptors ( )
			       .InterceptedBy ( typeof ( LoggingInterceptor ) ) ;
			#endregion

		}


		/// <summary>
		/// Override to attach module-specific functionality to a
		/// component registration.
		/// </summary>
		/// <remarks>This method will be called for all existing <i>and future</i> component
		/// registrations - ordering is not important.</remarks>
		/// <param name="componentRegistry">The component registry.</param>
		/// <param name="registration">The registration to attach functionality to.</param>
		protected override void AttachToComponentRegistration (
			IComponentRegistry     componentRegistry
		  , IComponentRegistration registration
		)
		{
			base.AttachToComponentRegistration ( componentRegistry , registration ) ;
		}

		/// <summary>
		/// Override to perform module-specific processing on a registration source.
		/// </summary>
		/// <remarks>This method will be called for all existing <i>and future</i> sources
		/// - ordering is not important.</remarks>
		/// <param name="componentRegistry">The component registry into which the source was added.</param>
		/// <param name="registrationSource">The registration source.</param>
		protected override void AttachToRegistrationSource (
			IComponentRegistry  componentRegistry
		  , IRegistrationSource registrationSource
		)
		{
			base.AttachToRegistrationSource ( componentRegistry , registrationSource ) ;
		}
	}
}