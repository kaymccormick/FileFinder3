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
using System.Reflection ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Registration ;
using Autofac.Extras.DynamicProxy ;
using WpfApp1.Logging ;
using WpfApp1.Menus ;
using Module = Autofac.Module ;

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
	}
}