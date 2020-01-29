using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Reflection ;
using System.Windows ;
using AppShared ;
using AppShared.Interfaces ;
using AppShared.Modules ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Lifetime ;
using Autofac.Extras.AttributeMetadata ;
using Castle.DynamicProxy ;
using Common.Logging ;
using NLog ;

namespace Common.Utils
{
	public static class ContainerHelper
	{
		public static ProxyGenerator ProxyGenerator = new ProxyGenerator ( ) ;

		private static readonly Logger Logger =
			LogManager.GetLogger ( "Autofac container builder helper" ) ;

		public static ILifetimeScope SetupContainer (
			out    IContainer container)
		{
			var scan = GetScanAssem ( ) ;
			return SetupContainer ( out container , scan ) ;
		}


		public static ILifetimeScope SetupContainer ( out IContainer container, IEnumerable <Assembly> assembliesToScan)
		{
			Logger.Info ("Assemblies " +
			             String.Join (
			                          ", "
			                        , assembliesToScan.Select ( ( assembly , i1 ) => assembly.GetName ( ).Name )
			                         )
			            ) ;
			AppLoggingConfigHelper.EnsureLoggingConfigured ( );
			var proxyGenerator = ProxyGenerator ;
			var builderInterceptor = new BuilderInterceptor ( proxyGenerator ) ;
			var proxy =
				proxyGenerator.CreateClassProxy < ContainerBuilder > ( builderInterceptor ) ;

			var proxyGenerationOptions = new ProxyGenerationOptions ( ) ;// new Hook ( ) ) ;


			var builder = proxy ;
			#region Autofac Modules
			builder.RegisterModule<AttributedMetadataModule> ( );
			builder.RegisterModule<MenuModule> ( );
			ModuleRegistrationExtensions.RegisterModule <IdGeneratorModule> ( ( ContainerBuilder ) builder );
			#endregion

			int i = 0 ;
			Action < int > dump = delegate ( int index ) {
				builderInterceptor.Invocations.ForEach (
				                                        invocation
					                                        => Logger.Debug (
					                                                         $"{index}]: {invocation.Method.Name} ({string.Join ( ", " , invocation.Arguments )}) => {invocation.OriginalReturnValue}"
					                                                        )
				                                       ) ;
				return ;
			} ;

			
			dump ( i );
			i += 1 ;



			// var obIdGenerator = new ObjectIDGenerator();

			// builder.Register < ObjectIDGenerator > ( ).InstancePerLifetimeScope ( ) ;
			#region Currently unused?
			//builder.RegisterType<SystemParametersControl> ( ).As<ISettingsPanel> ( );
			#endregion

			#region Assembly scanning
			var executingAssembly = Assembly.GetExecutingAssembly ( ) ;

			RegistrationExtensions.RegisterAssemblyTypes ( builder , assembliesToScan.ToArray())			                      .Where (
			                              delegate ( Type t ) {
				                              var isAssignableFrom = typeof ( Window ).IsAssignableFrom ( t ) ;
				                              Logger.Trace ( $"{t} is assignable from {isAssignableFrom}" );
				                              return isAssignableFrom;
			                              }
			                             )
			                      .AsSelf ( )
			                      //.As<Window> ( )
			                      .OnActivating (
			                                     args => {
				                                     var argsInstance = args.Instance ;

				                                     var haveLogger = argsInstance as IHaveLogger ;
				                                     if ( haveLogger != null )
				                                     {
					                                     haveLogger.Logger =
						                                     args.Context.Resolve < NLog.ILogger > (
						                                                                            new
							                                                                            TypedParameter (
							                                                                                            typeof
							                                                                                            ( Type
							                                                                                            )
							                                                                                          , argsInstance
								                                                                                           .GetType ( )
							                                                                                           )
						                                                                           ) ;
				                                     }
			                                     }
			                                    );

			// builder.RegisterAssemblyTypes ( executingAssembly )
			//        .Where ( predicate : t => typeof ( ITopLevelMenu ).IsAssignableFrom ( c : t ) )
			//        .As < ITopLevelMenu > ( ) ;
			#endregion

			// builder.RegisterType < AppTraceLisener > ( )
			//        .As < TraceListener > ( )
			//        .InstancePerLifetimeScope ( ) ;
			#region Interceptors
			builder.RegisterType<MyInterceptor> ( ).AsSelf ( );
			builder.RegisterType<LoggingInterceptor> ( ).AsSelf ( );
			#endregion

			#region Logging
			builder.RegisterType<LoggerTracker> ( )
			       .As<ILoggerTracker> ( )
			       .InstancePerLifetimeScope ( );

			// builder.RegisterType < LogFactory > ( ).AsSelf ( ) ;

			builder.Register (
			                  ( c , p ) => {
				                  var loggerName = "unset" ;
				                  try
				                  {
					                  loggerName = ParameterExtensions.TypedAs <Type> ( p ).FullName;
				                  }
				                  catch ( Exception ex )
				                  {
					                  Console.WriteLine ( ex.ToString ( ) );
				                  }

				                  var tracker = ResolutionExtensions.Resolve < ILoggerTracker > ( c ) ;
				                  Logger.Trace ( $"creating logger loggerName = {loggerName}" );
				                  var logger = LogManager.GetLogger ( loggerName ) ;
				                  tracker.TrackLogger ( loggerName , logger );
				                  return logger;
			                  }
			                 )
			       .As<ILogger> ( );
			#endregion


			#region Callbacks
			builder.RegisterBuildCallback ( c => Logger.Info ( "Container built." ) );
			builder.RegisterCallback (
			                          registry => {
				                          registry.Registered += ( sender , args ) => {
					                          Logger.Trace (
					                                        "Registered "
					                                        + args.ComponentRegistration.Activator
					                                              .LimitType
					                                       );
					                          args.ComponentRegistration.Activated += (
						                          o
						                        , eventArgs
					                          ) => {
						                          Logger.Trace (
						                                        $"Activated {DesribeComponent ( eventArgs.Component )} (sender={o}, instance={eventArgs.Instance})"
						                                       );
					                          };
				                          };
			                          }
			                         );
			#endregion

			#region Container Build
			var setupContainer = builder.Build ( ) ;
			container = setupContainer;
			setupContainer.ChildLifetimeScopeBeginning +=
				SetupContainerOnChildLifetimeScopeBeginning;
			#endregion
			setupContainer.CurrentScopeEnding += SetupContainerOnCurrentScopeEnding;


			// #region Post-container build reporting
			return setupContainer.BeginLifetimeScope ( "initial scope" );
			// ,configurationAction : containerBuilder
			// => ConfigurationAction (
			// containerBuilder
			// )
			// ) ;
			//return CreateChildLifetimeContext ( setupContainer ) ;
		}

		private static IEnumerable < Assembly > GetScanAssem ( )
		{
			return AppDomain.CurrentDomain.GetAssemblies ( )
			                .Where (
			                        ( assembly , i1 ) => Attribute.IsDefined (
			                                                                  assembly
			                                                                , typeof (
				                                                                  AssemblyContainerScanAttribute
			                                                                  )
			                                                                 )
			                       ) ;
		}

		private static void

			Target ( IComponentRegistry obj )
		{
			throw new NotImplementedException ( );
		}

		private static void SetupContainerOnCurrentScopeEnding (
			object                       sender
		  , LifetimeScopeEndingEventArgs e
		)
		{
			Logger.Info (
			             $"{nameof ( SetupContainerOnCurrentScopeEnding )} {e.LifetimeScope.Tag}"
			            );
		}

		private static void SetupContainerOnChildLifetimeScopeBeginning (
			object                          sender
		  , LifetimeScopeBeginningEventArgs e
		)
		{
			Logger.Info ( $"{sender} {e.LifetimeScope.Tag}" );
		}

		private static string DesribeComponent ( IComponentRegistration eventArgsComponent )
		{
			var debugDesc = "no description" ;
			var key = "DebugDescription" ;
			if ( eventArgsComponent.Metadata.ContainsKey ( key ) )
			{
				debugDesc = eventArgsComponent.Metadata[key].ToString ( );
			}

			return $" CompReg w({eventArgsComponent.Id}, {debugDesc})";
		}
#if DOIT

		private static DeferredCallback ConfigurationAction ( ContainerBuilder obj )
		{
			return ;
			// return obj.RegisterCallback ( CreateChildLifetimeContext ) ;
		}
			private static void CreateChildLifetimeContext ( IComponentRegistryBuilder componentRegistryBuilder )
		{
			// var setupContainer = componentRegistry ;
#if USEHANDLER
			setupContainer.ResolveOperationBeginning += ( sender , args ) => {
				args.ResolveOperation.InstanceLookupBeginning += ( o , eventArgs ) => {
					eventArgs.InstanceLookup.InstanceLookupEnding +=
 ( sender1 , endingEventArgs ) => {
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
							                               Logger.Debug (
							                                             "has IHaveLogger interface."
							                                            ) ;
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
						if ( componentRegistryRegistration.Ownership
						     == InstanceOwnership.ExternallyOwned )
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


							IComponentRegistration componentRegistration =
								new ComponentRegistration (
								                           Guid.NewGuid ( )
								                     , x
								                     , componentRegistryRegistration.Lifetime
								                     , componentRegistryRegistration.Sharing
								                     , componentRegistryRegistration.Ownership
								                     , componentRegistryRegistration.Services
								                     , componentRegistryRegistration.Metadata
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
#endif
		public static void Dump
			(
			IComponentRegistration componentRegistryRegistration
		  , HashSet<object>        seenObjects, Action<string> outFunc
		)
		{
			var activatorLimitType = componentRegistryRegistration.Activator.LimitType ;
			Logger _logger = LogManager.GetLogger ( activatorLimitType.FullName ) ;

			if ( seenObjects.Contains ( componentRegistryRegistration ) )
			{
				return;
			}

			seenObjects.Add ( componentRegistryRegistration );
			outFunc( "Id = " + componentRegistryRegistration.Id );
			outFunc(
			               "Activator type = " + componentRegistryRegistration.Activator.GetType ( )
			              );




			componentRegistryRegistration.Activator.GetType ( );

			outFunc( "LimitType = " + activatorLimitType );


			foreach ( var service in componentRegistryRegistration.Services )
			{
				outFunc( "Service is " + service.Description );
			}

			if ( componentRegistryRegistration.Target == null )
			{
				outFunc( "Target registration is null." );
			}
			else if ( Equals (
			                  componentRegistryRegistration
			                , componentRegistryRegistration.Target
			                 ) )
			{
				outFunc( "Target is same registration." );
			}
			else
			{
				Dump ( componentRegistryRegistration.Target , seenObjects, outFunc );
			}
		}
	}
}