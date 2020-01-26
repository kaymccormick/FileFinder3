using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection ;
using System.Runtime.Serialization ;
using System.Text;
using System.Threading.Tasks;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using WpfApp1.DefaultServices ;
using WpfApp1.Util ;
using Module = Autofac.Module ;

namespace WpfApp1
{
	public class IdGeneratorModule : Module
	{
		private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger ( ) ;
		/// <summary>Override to add registrations to the container.</summary>
		/// <remarks>
		/// Note that the ContainerBuilder parameter is unique to this module.
		/// </remarks>
		/// <param name="builder">The builder through which components can be
		/// registered.</param>
		protected override void Load ( ContainerBuilder builder )
		{

			//var obIdGenerator = new ObjectIDGenerator();
			Logger.Info ( ( nameof ( IdGeneratorModule ) ) ) ;
		    generator = new ObjectIDGenerator();
			//builder.RegisterInstance ( generator ).As < ObjectIDGenerator > ( ) ;
//			builder.RegisterType < ObjectIDGenerator > ( ).InstancePerLifetimeScope ( ).AsSelf ( ) ;
		    defaultObject = new DefaultObjectIdProvider(generator);
			builder.RegisterInstance(defaultObject).As<IObjectIdProvider> (  ).SingleInstance();
			// builder.RegisterType < DefaultObjectIdProvider > ( )
			//        .As < IObjectIdProvider > ( )
			//        .InstancePerLifetimeScope ( ) ;

		}

		public DefaultObjectIdProvider defaultObject { get ; set ; }

		public ObjectIDGenerator generator { get ; set ; }

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
			registration.Preparing += RegistrationOnPreparing;
			registration.Activating += RegistrationOnActivating;
		}

		private void RegistrationOnActivating ( object sender , ActivatingEventArgs < object > e )
		{
			var inst = e.Instance;
			
			;
			Logger.Debug ( $"{nameof ( RegistrationOnActivating )} {e.Component}" ) ;
			if ( e.Component.Services.Any (
			                               service
				                               => {
				                               var typedService = service as TypedService ;
				                               Logger.Debug ( typedService ) ;
				                               if ( typedService != null )
				                               {
					                               var typedServiceServiceType = typedService.ServiceType ;
					                               return typedServiceServiceType
					                                      == typeof ( ObjectIDGenerator ) ;
				                               }

				                               return false ;
			                               }
			                              ) )
			{
				return ;
			}
			//var provider = e.Context.Resolve < IObjectIdProvider > ( ) ;
			var provideObjectInstanceIdentifier = defaultObject.ProvideObjectInstanceIdentifier (inst, e.Component, e.Parameters ) ;
			if ( inst is IHaveObjectId x )
			{
				x.InstanceObjectId = provideObjectInstanceIdentifier ;
			}
		}

		private void RegistrationOnPreparing ( object sender , PreparingEventArgs e )
		{
			// e.Parameters = e.Parameters.Union (
			//                                    new[]
			//                                    {
			// 	                                   new ResolvedParameter (
			// 	                                                          ( info , context )
			// 		                                                          => info.ParameterType
			// 		                                                             == typeof (
			// 			                                                             IObjectIdProvider )
			// 	                                                        , ( info , context ) => {
			// 		                                                          return
			// 			                                                          null ; //context.
			// 	                                                          }
			// 	                                                         )
			//                                    }
			//                                   ) ;
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
