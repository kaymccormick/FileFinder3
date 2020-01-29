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
using Autofac.Extras.DynamicProxy ;
using Common.Menus ;
using Module = Autofac.Module ;

namespace Common
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
			       //.AsImplementedInterfaces ( )
			       .WithMetadata < 
				        ResourceMetadata
			        > ( configurationAction : m => m.For ( propertyAccessor : rn => rn.ResourceName , value : "MenuItemList" ) )
			       .PreserveExistingDefaults ( )
			      .As<IMenuItemList>()
			       .EnableInterfaceInterceptors ( )
			       .InterceptedBy ( typeof ( LoggingInterceptor ) ) ;
			builder.RegisterType < XMenuItem > ( )
//			       .AsImplementedInterfaces ( )
			       .PreserveExistingDefaults ( )
			       .EnableInterfaceInterceptors ( )
			       .InterceptedBy ( typeof ( LoggingInterceptor ) ) ;
			#endregion

		}
	}
}