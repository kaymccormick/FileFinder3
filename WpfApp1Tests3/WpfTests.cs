#define LOG_HERE
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Baml2006;
using System.Windows.Markup;
using Autofac;
using JetBrains.Annotations;
using NLog;
using NLog.Config;
using NLog.Fluent;
using NLog.Targets;
using WpfApp1.Interfaces;
using WpfApp1Tests3.Attributes;
using WpfApp1Tests3.Fixtures;
using WpfApp1Tests3.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WpfApp1Tests3
{
    [ Collection( "WpfApp" ) ]
    public class WpfTests
        : IClassFixture < ContainerFixture >, //, IClassFixture <MyServicesFixture>,
            IDisposable,
            IHasId
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public WpfTests(
            WpfApplicationFixture fixture,
            ContainerFixture      containerFixture,
            ObjectIDFixture       objectIdFixture,
            UtilsContainerFixture utilsContainerFixture,
            ITestOutputHelper     outputHelper
        )
        {
#if LOG_HERE
            MyTarget = new XunitTarget( outputHelper );
            LogManager.Configuration.AddTarget( "xunit", MyTarget );
            //LogManager.Configuration.AddRule( LogLevel.Debug, LogLevel.Fatal, MyTarget, "*" );
            //LogManager.Configuration.LoggingRules.Add( new LoggingRule( "*", LogLevel.Debug, MyTarget ));
            LogManager.Configuration.LoggingRules.Insert( 0, new LoggingRule( "*", LogLevel.FromString( "Trace" ), MyTarget ) );
            LogManager.ReconfigExistingLoggers();

            Logger.Debug( "test" );
            MyTarget.Write( LogEventInfo.Create( LogLevel.Info, "test", "beep" ) );
#endif
            _myServicesFixture = utilsContainerFixture.Container.Resolve < MyServicesFixture >();
            //ContextStack<InfoContext>.DefaultAllowDuplicateNames = false; Instances.Add( this );
            Fixture                = fixture;
            ObjectIdFixture        = objectIdFixture;
            OutputHelper           = outputHelper;
            _containerFixture      = containerFixture;
            _utilsContainerFixture = utilsContainerFixture;
            MyStack                = InstanceFactory.CreateContextStack < InfoContext >();
            bool firstTime;
            ObjectId        = Generator.GetId( this, out firstTime );
            Instances[this] = ObjectId;

            Assert.True( firstTime );
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing,
        ///     or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            long myid;
            Instances.TryRemove( this, out myid );
#if LOG_HERE
            LogManager.Configuration.RemoveTarget( "xunit" );
            LogManager.Configuration.LoggingRules.RemoveAt( 0 );
            LogManager.ReconfigExistingLoggers();

#endif
        }

        public class MyServices : IMyServices
        {
            public MyServices(
                InfoContext.Factory infoContextFactory
            )
            {
                InfoContextFactory = infoContextFactory;
            }

            public InfoContext.Factory InfoContextFactory { get; }
        }

        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public WpfApplicationFixture Fixture { get; }

        public ObjectIDFixture ObjectIdFixture { get; }

        public ITestOutputHelper OutputHelper { get; }

        private readonly ContainerFixture      _containerFixture;
        private readonly UtilsContainerFixture _utilsContainerFixture;
        private readonly MyServicesFixture     _myServicesFixture;

        // ReSharper disable once MemberCanBePrivate.Global
        [ ContextStackInstance ] public ContextStack < InfoContext > MyStack { get; }

        [ InfoContextFactory ]
        [ UsedImplicitly ]
        public InfoContext.Factory InfoContextFactory => myServices.InfoContextFactory;

        [ThreadStatic]
        public static ConcurrentDictionary < object, long > Instances = new ConcurrentDictionary < object, long >();

        public ObjectIDFixture.GetObjectIdDelegate GetObjIdFunc => ObjectIdFixture.GetObjectId;

        public ObjectIDGenerator Generator => ObjectIdFixture.Generator;

        private IComponentContext UtilsContainer => _utilsContainerFixture.Container;

        private IMyServices myServices => _myServicesFixture.MyServices;

        public XunitTarget MyTarget { get; set; }

        public Factory InstanceFactory => ObjectIdFixture.InstanceFactory;

        private IDisposable C(
            object test,
            [CanBeNull] string name = null
        ) => new AttachedContext(MyStack, InfoContextFactory(name, test));


        private void DoLog(
            string test
        )
        {
            var logBuilder =
                Log.Warn().Message( test );
            logBuilder = logBuilder.Property( "stack", MyStack );
            //.Property( "context", MyStack.ToOrderedDictionary()) //.Property("stack", MyStack)

            logBuilder.Write();
        }

        private void DumpResource(
            ContextStack < InfoContext > context,
            object                       resource
        )
        {
            context.Push( myServices.InfoContextFactory( "resource", resource ) );
            if ( resource is Style style )
            {
                Logger.Debug( $"{{@context}} : TargetType = {style.TargetType}", new { Context = context } );
                foreach ( var setter in style.Setters )
                {
                    switch ( setter )
                    {
                        case Setter s:
                            Logger.Debug( $"{context} : Setter" );
                            DumpDependencyProperty( context, s.Property );
                            Logger.Debug( $"TargetName = {s.TargetName}" );
                            Logger.Debug( $"Value = {s.Value}" );
                            DumpValue( context, s.Value );
                            break;
                        case EventSetter eventSetter:
                            Logger.Debug( $"{context} : EventSetter.Event = {eventSetter.Event}" );
                            Logger.Debug( $"{context} : HandledEventsToo = {eventSetter.HandledEventsToo}" );
                            Logger.Debug( $"{context} : Method {eventSetter.Handler.Method}" );
                            Logger.Debug( $"{context} : Target {eventSetter.Handler.Target}" );
                            break;
                    }
                }
            }

            context.Pop();
        }

        private void DumpValue(
            ContextStack < InfoContext > context,
            object                       sValue
        )
        {
            context.Push( myServices.InfoContextFactory( "value", sValue ) );

            var prefix = context.ToString();
            switch ( sValue )
            {
                case DynamicResourceExtension d:
                    Logger.Debug( $"{prefix} : Value Type {d.GetType()}" );
                    Logger.Debug( $"{prefix} : Resource Key {d.ResourceKey}" );
                    var provideValue = d.ProvideValue( new ServiceProviderProxy() );
                    DumpProvidedValue( context, provideValue );

                    Logger.Debug( $"ProvideValue is {provideValue}" );
                    break;
                default:
                    Logger.Debug( "Value: " );
                    break;
            }

            context.Pop();
        }

        private void DumpProvidedValue(
            ContextStack < InfoContext > context,
            object                       provideValue
        )
        {
            var prefix = context.ToString();
            Logger.Debug( $"{prefix} : type of provided value is {provideValue.GetType()}" );
            var typeConverter = TypeDescriptor.GetConverter( provideValue );
            context.Push( myServices.InfoContextFactory( "provideValue", provideValue ) );
            if ( typeConverter.CanConvertTo( typeof(string) ) )
            {
                var convertTo = typeConverter.ConvertTo( provideValue, typeof(string) );
                Logger.Debug( $"{prefix} : converted to {convertTo}" );
            }

            foreach ( var p in provideValue.GetType().GetFields( BindingFlags.NonPublic | BindingFlags.Instance ) )
            {
                Logger.Debug( $"{prefix} : field {p.Name} = {p.GetValue( provideValue )}" );
            }

            foreach ( var p in provideValue.GetType().GetProperties( BindingFlags.NonPublic | BindingFlags.Instance ) )
            {
                Logger.Debug( $"{prefix} : property {p.Name} = {p.GetValue( provideValue )}" );
            }

            context.Pop();
        }

        private void DumpDependencyProperty(
            ContextStack < InfoContext > context,
            DependencyProperty           sProperty
        )
        {
            context.Push( myServices.InfoContextFactory( "DependencyProperty", sProperty ) );
            var prefix = context.ToString();
            Logger.Debug( $"{prefix} : DependencyProperty: {sProperty.Name}" );
            Logger.Debug( $"{prefix} : DependencyProperty.PropertyType: {sProperty.PropertyType}" );
            Logger.Debug( $"{prefix} : DependencyProperty.OwnerType: {sProperty.OwnerType}" );
        }

        public long ObjectId { get; }

        [ Fact ]
        public void Test1()
        {
            var candidateConfigFilePaths = LogManager.LogFactory.GetCandidateConfigFilePaths();
            foreach ( var q in candidateConfigFilePaths )
            {
                Logger.Debug( $"{q}" );
            }

            var loggingConfiguration = LogManager.Configuration;
            var fieldInfo = loggingConfiguration
                           .GetType().GetField( "_originalFileName", BindingFlags.NonPublic | BindingFlags.Instance );
            if ( fieldInfo != null )
            {
                Logger.Debug( $"Original NLOG configuration filename is {fieldInfo.GetValue( loggingConfiguration )}" );
            }

            Logger.Debug( $"{loggingConfiguration}" );
            using (var scope = _containerFixture.LifetimeScope.BeginLifetimeScope())
            {
                var menuItemList = scope.Resolve < IMenuItemList >();
                Assert.NotNull( menuItemList );
                Assert.NotEmpty( menuItemList );
                Fixture.MyApp.Resources["MyMenuItemList"] = menuItemList;
                var found = Fixture.MyApp.FindResource( "MyMenuItemList" );
                Assert.NotNull( found );
                var x = string.Join( ", ", menuItemList.First().Children );
                Logger.Debug( $"found {found}, {x}" );
                var uri = new Uri( Fixture.BasePackUri, "Resources/MenuResources.xaml" );
                Logger.Debug( $"{uri}" );

                var stream = Application.GetResourceStream( uri );
                Logger.Info( stream.ContentType );
                var baml2006Reader = new Baml2006Reader( stream.Stream );


                var o = XamlReader.Load( baml2006Reader );
                ///var o = Application.LoadComponent( uri );
                var menuResources = o as ResourceDictionary;

                //var stack = InstanceFactory.CreateContextStack < InfoContext >();
                var stack = MyStack;
                var entry = myServices.InfoContextFactory( nameof( menuResources ), menuResources );
                ;
                stack.Push( entry );

                foreach ( var q in menuResources.Keys )
                {
                    var resource = menuResources[q];
                    stack.Push( myServices.InfoContextFactory( "key", q ) );
                    Logger.Debug( $"{q}: {resource}" );
                    var prefix = $"Resource[{q}]";
                    DumpResource( stack, resource );
                    stack.Pop();
                }
            }
        }

        [ Fact ]
        [ Trait( "MSBuild", "Include" ) ]
        [ PushContext( "my pushed context" ) ]
        public void Test2()
        {
            using var c = C( "using context" );
            DoLog( "This is my excellent log message." );
        }

        [ Fact ]
        [ Trait( "MSBuild", "Include" ) ]
        public void Test3()
        {
            using var c = C( "testxx" );
            using var c2 = C( "testxx2" );
            DoLog( "test" );
        }
    }

    public class MyServicesFixture
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public MyServicesFixture(
            IMyServices myServices
        )
        {
            MyServices = myServices;
        }

        public IMyServices MyServices { get; set; }
    }

    public interface IMyServices

    {
        InfoContext.Factory InfoContextFactory { get; }
    }

    internal class ServiceProviderProxy : IServiceProvider
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        /// <summary>Gets the service object of the specified type.</summary>
        /// <param name="serviceType">
        ///     An object that specifies the type of service object
        ///     to get.
        /// </param>
        /// <returns>
        ///     A service object of type <paramref name="serviceType" />.
        ///     -or-
        ///     <see langword="null" /> if there is no service object of type
        ///     <paramref name="serviceType" />.
        /// </returns>
        public object GetService(
            Type serviceType
        )
        {
            Logger.Debug( $"{nameof( GetService )} {serviceType}" );
            if ( serviceType == typeof(IProvideValueTarget) )
            {
                return new ProvideValueTarget( null, null );
            }

            throw new NotImplementedException();
        }
    }

    internal class ProvideValueTarget : IProvideValueTarget
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Object" />
        ///     class.
        /// </summary>
        public ProvideValueTarget(
            object targetObject,
            object targetProperty
        )
        {
            TargetObject   = targetObject;
            TargetProperty = targetProperty;
        }

        /// <summary>Gets the target object being reported.</summary>
        /// <returns>The target object being reported.</returns>
        public object TargetObject { get; }

        /// <summary>Gets an identifier for the target property being reported.</summary>
        /// <returns>An identifier for the target property being reported.</returns>
        public object TargetProperty { get; }
    }

    public class AttachedContext : IDisposable
    {
        private readonly InfoContext                  _infoContext;
        private readonly ContextStack < InfoContext > contextStack;

        public AttachedContext(
            ContextStack < InfoContext > contextStack,
            InfoContext                  context
        )
        {
            this.contextStack = contextStack;
            _infoContext      = context;
            contextStack.Push( _infoContext );
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing,
        ///     or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Assert.NotEmpty( contextStack );
            Assert.True( ReferenceEquals( _infoContext, contextStack.First() ) );
            contextStack.Pop();
        }
    }

    [ Target( "Xunit" ) ]
    public class XunitTarget : TargetWithLayout
    {
        private readonly ITestOutputHelper _outputHelper;

        public XunitTarget(
            ITestOutputHelper outputHelper
        )
        {
            _outputHelper = outputHelper;
        }

        /// <summary>
        ///     Writes logging event to the log target. Must be overridden in inheriting
        ///     classes.
        /// </summary>
        /// <param name="logEvent">Logging event to be written out.</param>
        public new void Write(
            LogEventInfo logEvent
        )
        {
            var renderLogEvent = RenderLogEvent( Layout, logEvent );
            _outputHelper.WriteLine( renderLogEvent );
        }
    }
}