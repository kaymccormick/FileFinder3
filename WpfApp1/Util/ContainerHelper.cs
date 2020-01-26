using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.Reflection ;
using System.Windows ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Builder ;
using Autofac.Core ;
using Autofac.Core.Activators.Delegate ;
using Autofac.Core.Activators.Reflection ;
using Autofac.Core.Lifetime ;
using Autofac.Core.Registration ;
using Autofac.Extras.AttributeMetadata ;
using NLog ;
using WpfApp1.Application ;
using WpfApp1.Controls ;
using WpfApp1.Logging ;

namespace WpfApp1.Util
{
	public static class ContainerHelper
	{
		private static readonly Logger Logger =
			LogManager.GetLogger ( "Autofac container builder helper" ) ;

		public static ILifetimeScope SetupContainer ( )
		{
			AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ;
			var builder = new ContainerBuilder ( ) ;
			#region Autofac Modules
			builder.RegisterModule < AttributedMetadataModule > ( ) ;
			builder.RegisterModule < MenuModule > ( ) ;
			builder.RegisterModule < IdGeneratorModule > ( ) ;
			#endregion

			// var obIdGenerator = new ObjectIDGenerator();

			// builder.Register < ObjectIDGenerator > ( ).InstancePerLifetimeScope ( ) ;
			#region Currently unused?
			builder.RegisterType < SystemParametersControl > ( ).As < ISettingsPanel > ( ) ;
			#endregion

			#region Assembly scanning
			var executingAssembly = Assembly.GetExecutingAssembly ( ) ;

			builder.RegisterAssemblyTypes ( executingAssembly )
			       .Where (
			               predicate : delegate ( Type t ) {
				               var isAssignableFrom = typeof ( Window ).IsAssignableFrom ( c : t ) ;
				               Logger.Trace ( message : $"{t} is assignable from {isAssignableFrom}" ) ;
				               return isAssignableFrom ;
			               }
			              )
			       .AsSelf ( )
			       .As < Window > ( )
			       .OnActivating (
								  
			                      handler : args => {
				                      var argsInstance = args.Instance ;
				                      ( argsInstance as IHaveLogger ).Logger =
					                      args.Context.Resolve < ILogger > (
					                                                        new TypedParameter (
					                                                                            type : typeof
					                                                                            ( Type
					                                                                            )
					                                                                          , value : argsInstance
					                                                                                   .GetType ( )
					                                                                           )
					                                                       ) ;
			                      }
			                     ) ;

			// builder.RegisterAssemblyTypes ( executingAssembly )
			//        .Where ( predicate : t => typeof ( ITopLevelMenu ).IsAssignableFrom ( c : t ) )
			//        .As < ITopLevelMenu > ( ) ;
			#endregion

			// builder.RegisterType < AppTraceLisener > ( )
			//        .As < TraceListener > ( )
			//        .InstancePerLifetimeScope ( ) ;
			#region Interceptors
			builder.RegisterType < MyInterceptor > ( ).AsSelf ( ) ;
			builder.RegisterType < LoggingInterceptor > ( ).AsSelf ( ) ;
			#endregion

			#region Logging
			builder.RegisterType < LoggerTracker > ( )
			       .As < ILoggerTracker > ( )
			       .InstancePerLifetimeScope ( ) ;

			// builder.RegisterType < LogFactory > ( ).AsSelf ( ) ;

			builder.Register (
			                  @delegate : ( c , p ) => {
				                  var loggerName = "unset" ;
				                  try
				                  {
					                  loggerName = p.TypedAs < Type > ( ).FullName ;
				                  }
				                  catch ( Exception ex )
				                  {
					                  Console.WriteLine ( value : ex.ToString ( ) ) ;
				                  }

				                  var tracker = c.Resolve < ILoggerTracker > ( ) ;
				                  Logger.Debug ( message : $"creating logger loggerName = {loggerName}" ) ;
				                  var logger = LogManager.GetLogger ( name : loggerName ) ;
				                  tracker.TrackLogger ( loggerName : loggerName , logger : logger ) ;
				                  return logger ;
			                  }
			                 )
			       .As < ILogger > ( ) ;
			#endregion

			
			#region Callbacks
			builder.RegisterBuildCallback ( buildCallback : container => Logger.Info ( message : "Container built." ) ) ;
			builder.RegisterCallback (
			                          configurationCallback : registry => {
				                          registry.Registered += ( sender , args ) => {
					                          Logger.Debug (
					                                        message : "Registered "
					                                                  + args.ComponentRegistration.Activator
					                                                        .LimitType
					                                       ) ;
					                          args.ComponentRegistration.Activated += (
						                          o
						                        , eventArgs
					                          ) => {
						                          Logger.Debug (
						                                        $"Activated {DesribeComponent ( eventArgs.Component )} (sender={o}, instance={eventArgs.Instance})"
						                                       ) ;
					                          } ;
				                          } ;
			                          }
			                         ) ;
			#endregion

			#region Container Build
			var setupContainer = builder.Build ( ) ;
			setupContainer.ChildLifetimeScopeBeginning += SetupContainerOnChildLifetimeScopeBeginning;
			#endregion
			setupContainer.CurrentScopeEnding += SetupContainerOnCurrentScopeEnding;


			#region Post-container build reporting
			return setupContainer.BeginLifetimeScope ( "initial scope",configurationAction : containerBuilder
				                                           => ConfigurationAction (
				                                                                   containerBuilder
				                                                                  )
			                                         ) ;
			//return CreateChildLifetimeContext ( setupContainer ) ;
		}

		private static void SetupContainerOnCurrentScopeEnding (
			object                       sender
		  , LifetimeScopeEndingEventArgs e
		)
		{
			Logger.Info (
			             $"{nameof ( SetupContainerOnCurrentScopeEnding )} {e.LifetimeScope.Tag}"
			            ) ;
		}

		private static void SetupContainerOnChildLifetimeScopeBeginning (
			object                          sender
		  , LifetimeScopeBeginningEventArgs e
		)
		{
			Logger.Info ( $"{sender} {e.LifetimeScope.Tag}" ) ;
		}

		private static string DesribeComponent ( IComponentRegistration eventArgsComponent )
		{
			var debugDesc = "no description";
			var key = "DebugDescription" ;
			if ( eventArgsComponent.Metadata.ContainsKey ( key ) )
			{
				debugDesc = eventArgsComponent.Metadata[ key ].ToString ( ) ;
			}
			return
				$" CompReg w({eventArgsComponent.Id.ShortenGuid()}, {debugDesc})" ;
		}

		private static DeferredCallback ConfigurationAction ( ContainerBuilder obj )
		{
			return obj.RegisterCallback ( CreateChildLifetimeContext ) ;
		}

		private static void CreateChildLifetimeContext ( IComponentRegistry componentRegistry )
		{
			var setupContainer =  componentRegistry ;
			#if USEHANDLER
			setupContainer.ResolveOperationBeginning += ( sender , args ) => {
				args.ResolveOperation.InstanceLookupBeginning += ( o , eventArgs ) => {
					eventArgs.InstanceLookup.InstanceLookupEnding += ( sender1 , endingEventArgs ) => {
						if ( endingEventArgs.NewInstanceActivated )
						{
							Logger.Debug ( "New instance activated" ) ;
						}
					} ;
				} ;
			} ;
#endif

			var registry = componentRegistry ;
			foreach ( var componentRegistryRegistration in registry.Registrations )
			{
				if ( IsMyRegistration ( componentRegistryRegistration ) )
				{
					Logger.Warn ( "is my registration" ) ;
				}
				else
				{
					Logger.Error ( "is not my registration" ) ;
				}

				var seen = new HashSet < object > ( ) ;
				Dump ( componentRegistryRegistration , seen ) ;

				if ( componentRegistryRegistration.Activator is ReflectionActivator rf )
				{
					Logger.Debug ( rf.LimitType.ToString ( ) ) ;
					var x = new DelegateActivator (
					                               rf.LimitType
					                             , ( context , parameters ) => {
						                               Logger.Debug (
						                                             "delegate activation of reflection component success."
						                                            ) ;
						                               var r = rf.ActivateInstance (
						                                                            context
						                                                          , parameters
						                                                           ) ;
						                               Logger.Debug ( "got " + r ) ;
						                               if ( r is IHaveLogger haveLogger )
						                               {
							                               Logger.Debug ( "has IHaveLogger interface." ) ;
							                               if ( haveLogger.Logger == null )
							                               {
								                               Logger.Debug (
								                                             "logger is null, resolving"
								                                            ) ;
								                               haveLogger.Logger =
									                               context.Resolve < ILogger > (
									                                                            new
										                                                            TypedParameter (
										                                                                            typeof
										                                                                            ( Type
										                                                                            )
										                                                                          , r
											                                                                           .GetType ( )
										                                                                           )
									                                                           ) ;
							                               }
						                               }

						                               return r ;
					                               }
					                              ) ;

					IComponentRegistration componentRegistration = new ComponentRegistration (
					                                                                          Guid
						                                                                         .NewGuid ( )
					                                                                        , x
					                                                                        , componentRegistryRegistration
						                                                                         .Lifetime
					                                                                        , componentRegistryRegistration
						                                                                         .Sharing
					                                                                        , componentRegistryRegistration
						                                                                         .Ownership
					                                                                        , componentRegistryRegistration
						                                                                         .Services
					                                                                        , componentRegistryRegistration
						                                                                         .Metadata
					                                                                        , componentRegistryRegistration
					                                                                         ) ;

					Logger.Debug ( "wrapping reflection with delegate" ) ;
					try
					{
						registry.Register ( componentRegistration ) ;
					}
					catch ( Exception ex )
					{
						Logger.Debug ( ex , "failure is " + ex.Message ) ;
					}
				}
				else if ( componentRegistryRegistration.Activator is DelegateActivator d )
				{
					if ( ! ( d is MyActivator ) )
					{
						if ( componentRegistryRegistration.Ownership == InstanceOwnership.ExternallyOwned )
						{
							Logger.Debug ( "Externally owned component registration." ) ;
						}
						else
						{
							var x = new DelegateActivator (
							                               d.LimitType
							                             , ( context , parameters ) => {
								                               Logger.Debug ( "activating !!" ) ;
								                               var r = d.ActivateInstance (
								                                                           context
								                                                         , parameters
								                                                          ) ;
								                               Logger.Debug ( "got " + r ) ;
								                               return r ;
							                               }
							                              ) ;


							IComponentRegistration componentRegistration = new ComponentRegistration (
							                                                                          Guid
								                                                                         .NewGuid ( )
							                                                                        , x
							                                                                        , componentRegistryRegistration
								                                                                         .Lifetime
							                                                                        , componentRegistryRegistration
								                                                                         .Sharing
							                                                                        , componentRegistryRegistration
								                                                                         .Ownership
							                                                                        , componentRegistryRegistration
								                                                                         .Services
							                                                                        , componentRegistryRegistration
								                                                                         .Metadata
							                                                                        , componentRegistryRegistration
							                                                                         ) ;


							Logger.Debug ( "wrapping delegate activator" ) ;
							try
							{
								registry.Register ( componentRegistration ) ;
							}
							catch ( Exception ex )
							{
								Logger.Debug ( ex , "failure is " + ex.Message ) ;
							}
						}
					}
				}

				componentRegistryRegistration.Preparing += ( sender , args ) => {
				} ;

				componentRegistryRegistration.Activated += ( sender , args ) => {
					Logger.Debug ( $"Activated {args.Instance}" ) ;
				} ;
			}
			#endregion
		}

		private static bool IsMyRegistration (
			IComponentRegistration componentRegistryRegistration
		)
		{
			return GetIsMyAssembly ( componentRegistryRegistration.Activator.LimitType.Assembly ) ;
		}

		private static bool GetIsMyAssembly ( Assembly limitTypeAssembly )
		{
			Logger.Debug ( $"{limitTypeAssembly.GetName ( )}" ) ;
			if ( AssemblyName.ReferenceMatchesDefinition (
			                                              limitTypeAssembly.GetName ( )
			                                            , Assembly
			                                             .GetExecutingAssembly ( )
			                                             .GetName ( )
			                                             ) )
			{
				return true ;
			}

			return false ;
		}

		private static void Dump (
			IComponentRegistration componentRegistryRegistration
		  , HashSet < object >     seenObjects
		)
		{
			var activatorLimitType = componentRegistryRegistration.Activator.LimitType ;
			ILogger _logger = LogManager.GetLogger ( activatorLimitType.FullName ) ;

			if ( seenObjects.Contains ( componentRegistryRegistration ) )
			{
				return ;
			}

			seenObjects.Add ( componentRegistryRegistration ) ;
			_logger.Debug ( "Id = " + componentRegistryRegistration.Id ) ;
			_logger.Debug (
			               "Activator type = " + componentRegistryRegistration.Activator.GetType ( )
			              ) ;

			
			

	componentRegistryRegistration.Activator.GetType (
			              ) ;

			_logger.Debug ( "LimitType = " + activatorLimitType ) ;


			foreach ( var service in componentRegistryRegistration.Services )
			{
				_logger.Debug ( "Service is " + service.Description ) ;
			}

			if ( componentRegistryRegistration.Target == null )
			{
				Logger.Debug ( "Target registration is null." ) ;
			}
			else if ( Equals (
			                  componentRegistryRegistration
			                , componentRegistryRegistration.Target
			                 ) )
			{
				Logger.Debug ( "Target is same registration." ) ;
			}
			else
			{
				Dump ( componentRegistryRegistration.Target , seenObjects ) ;
			}
		}
	}
}
