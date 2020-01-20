using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Baml2006;
using System.Windows.Markup;
using Autofac;
using NLog;
using WpfApp1.Interfaces;
using WpfApp1.Menus;
using NLog.Fluent;
using Xunit;
using Xunit.Sdk;

namespace WpfApp1Tests3
{
    [Collection("WpfApp")]
    public partial class WpfTests : IClassFixture<ContainerFixture>, IDisposable
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public GenericApplicationFixture fixture;
        private readonly ContainerFixture _containerFixture;

        [ContextStackInstance]
        public ContextStack < InfoContext > MyStack { get; }
        public static HashSet <object> Instances { get; } = new HashSet < object >();
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WpfTests(
            GenericApplicationFixture fixture,
            ContainerFixture containerFixture
        )
        {
            //ContextStack<InfoContext>.DefaultAllowDuplicateNames = false; Instances.Add( this );
            this.fixture = fixture;
            _containerFixture = containerFixture;
            MyStack = new ContextStack<InfoContext>();


            

        }

        [ Fact, Trait( "MSBuild", "Include" ), 
          PushContext( "my pushed context" ) ]
        public void Test2()
        {
            using var c = C("using context");
            DoLog("This is my excellent log message.");
        }

        [Fact]
        [Trait( "MSBuild", "Include")]
        public void Test3()
        {
            using var c = C("testxx");
            using var c2 = C("testxx2");
            DoLog("test");
        }

        private IDisposable C(
            object test
        )
        {
            var r = new AttachedContext( MyStack, test );
            return r;
        }


        private void DoLog(
            string test
        )
        {
            var logBuilder =
                    Log.Warn().Message( test );
                logBuilder = logBuilder.Property("stack", MyStack);
                //.Property( "context", MyStack.ToOrderedDictionary()) //.Property("stack", MyStack)
                
            logBuilder.Write();
        }

        [Fact]
        public void Test1()
        {
            var candidateConfigFilePaths = LogManager.LogFactory.GetCandidateConfigFilePaths();
            foreach (var q in candidateConfigFilePaths)
            {
                Logger.Debug($"{q}");
            }
            var loggingConfiguration = LogManager.Configuration;
            var fieldInfo = loggingConfiguration.GetType().GetField("_originalFileName", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null)
            {
                Logger.Debug($"Original NLOG configuration filename is {fieldInfo.GetValue(loggingConfiguration)}");
            }
            Logger.Debug($"{loggingConfiguration}");
            using (var scope = _containerFixture.LifetimeScope.BeginLifetimeScope())
            {
                var menuItemList = scope.Resolve<IMenuItemList>();
                Assert.NotNull(menuItemList);
                Assert.NotEmpty(menuItemList);
                fixture.MyApp.Resources["MyMenuItemList"] = menuItemList;
                var found = fixture.MyApp.FindResource("MyMenuItemList");
                Assert.NotNull(found);
                var x = String.Join(", ", menuItemList.First().Children);
                Logger.Debug($"found {found}, {x}");
                var uri = new Uri(fixture.BasePackUri, "Resources/MenuResources.xaml");
                Logger.Debug($"{uri}");

                var stream = Application.GetResourceStream(uri);
                Logger.Info(stream.ContentType);
                var baml2006Reader = new Baml2006Reader(stream.Stream);


                var o = XamlReader.Load(baml2006Reader);
                ///var o = Application.LoadComponent( uri );
                var menuResources = o as ResourceDictionary;

                var stack = new ContextStack<InfoContext>();
                stack.Push(new InfoContext(nameof(menuResources), menuResources));

                foreach (var q in menuResources.Keys)
                {
                    var resource = menuResources[q];
                    stack.Push(new InfoContext("key", q));
                    Logger.Debug($"{q}: {resource}");
                    var prefix = $"Resource[{q}]";
                    DumpResource(stack, resource);
                    stack.Pop();
                }


            }
        }

        private void DumpResource(
            ContextStack<InfoContext> context,
            object resource
        )
        {
            context.Push(new InfoContext("resource", resource));
            if (resource is Style style)
            {
                Logger.Debug($"{{@context}} : TargetType = {style.TargetType}", new { Context = context });
                foreach (var setter in style.Setters)
                {
                    switch (setter)
                    {
                        case Setter s:
                            Logger.Debug($"{context} : Setter");
                            DumpDependencyProperty(context, s.Property);
                            Logger.Debug($"TargetName = {s.TargetName}");
                            Logger.Debug($"Value = {s.Value}");
                            DumpValue(context, s.Value);
                            break;
                        case EventSetter eventSetter:
                            Logger.Debug($"{context} : EventSetter.Event = {eventSetter.Event}");
                            Logger.Debug($"{context} : HandledEventsToo = {eventSetter.HandledEventsToo}");
                            Logger.Debug($"{context} : Method {eventSetter.Handler.Method}");
                            Logger.Debug($"{context} : Target {eventSetter.Handler.Target}");
                            break;
                    }
                }
            }

            context.Pop();

        }

        private void DumpValue(
            ContextStack<InfoContext> context,
            object sValue
        )
        {
            context.Push(new InfoContext("value", sValue));

            var prefix = context.ToString();
            switch (sValue)
            {
                case DynamicResourceExtension d:
                    Logger.Debug($"{prefix} : Value Type {d.GetType()}");
                    Logger.Debug($"{prefix} : Resource Key {d.ResourceKey}");
                    var provideValue = d.ProvideValue(new ServiceProviderProxy());
                    DumpProvidedValue(context, provideValue);

                    Logger.Debug($"ProvideValue is {provideValue}");
                    break;
                default:
                    Logger.Debug("Value: ");
                    break;
            }

            context.Pop();
        }

        private void DumpProvidedValue(
            ContextStack<InfoContext> context,
            object provideValue
        )
        {
            var prefix = context.ToString();
            Logger.Debug($"{prefix} : type of provided value is {provideValue.GetType()}");
            var typeConverter = TypeDescriptor.GetConverter(provideValue);
            context.Push(new InfoContext("provideValue", provideValue));
            if (typeConverter.CanConvertTo(typeof(string)))
            {
                var convertTo = typeConverter.ConvertTo(provideValue, typeof(string));
                Logger.Debug($"{prefix} : converted to {convertTo}");
            }

            foreach (var p in provideValue.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Logger.Debug($"{prefix} : field {p.Name} = {p.GetValue(provideValue)}");
            }
            foreach (var p in provideValue.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Logger.Debug($"{prefix} : property {p.Name} = {p.GetValue(provideValue)}");
            }

            context.Pop();
        }

        private void DumpDependencyProperty(
            ContextStack<InfoContext> context,
            DependencyProperty sProperty
        )
        {
            context.Push(new InfoContext("DependencyProperty", sProperty));
            var prefix = context.ToString();
            Logger.Debug($"{prefix} : DependencyProperty: {sProperty.Name}");
            Logger.Debug($"{prefix} : DependencyProperty.PropertyType: {sProperty.PropertyType}");
            Logger.Debug($"{prefix} : DependencyProperty.OwnerType: {sProperty.OwnerType}");
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Instances.Remove( this );
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ContextStackInstanceAttribute
        : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Attribute" /> class.</summary>
        public ContextStackInstanceAttribute()
        {
        }
    }

    public class PushContextAttribute
        : BeforeAfterTestAttribute
    {
        public object Context { get; }
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        private InfoContext _context;
        private ContextStack < InfoContext > _stack;


        /// <summary>Initializes a new instance of the <see cref="T:System.Attribute" /> class.</summary>
        public PushContextAttribute(object context)
        {
            Context = context;
        }

        /// <summary>
        /// This method is called after the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public override void After(
            MethodInfo methodUnderTest
        )
        {
            Assert.NotEmpty(_stack);
            Assert.True(Object.ReferenceEquals(_stack.First(), _context)  );
            _stack.Pop();
        }

        /// <summary>
        /// This method is called before the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public override void Before(
            MethodInfo methodUnderTest
        )
        {
            var instances = WpfTests.Instances.Where( o => o.GetType() == methodUnderTest.DeclaringType );
            Assert.Single(instances);
            var instance = instances.Last();
            //var qq = from prop in instance.GetType().GetProperties() select Attribute.GetCustomAttributes(prop, typeof(ContextStackInstanceAttribute)) 
            var qq =
                from prop in instance.GetType().GetProperties()
                let atts = Attribute.GetCustomAttributes( prop, typeof(ContextStackInstanceAttribute) )
                from ContextStackInstanceAttribute att in atts
                select new { Prop = prop, Att = att };
            foreach ( var q in qq )
            {
                Logger.Debug( $"{q.Prop} {q.Att}" );
                var value = q.Prop.GetValue( instance );
                if ( value is ContextStack < InfoContext > stack )
                {
                    // thread safe?
                        _context = new InfoContext( "test1", Context );
                        _stack = stack;
                        _stack.Push( _context );
                    
                }
            }

            //var contextAtt = Attribute.GetCustomAttributes(instance.GetType(),)
            var customAttributes = Attribute.GetCustomAttributes(methodUnderTest, GetType());
            foreach ( PushContextAttribute q in customAttributes )
            {
                
            }
        }
    }

    class ServiceProviderProxy : IServiceProvider
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        /// <summary>Gets the service object of the specified type.</summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>A service object of type <paramref name="serviceType" />.
        /// -or-
        /// <see langword="null" /> if there is no service object of type <paramref name="serviceType" />.</returns>
        public object GetService(
            Type serviceType
        )
        {
            Logger.Debug($"{nameof(GetService)} {serviceType}");
            if (serviceType == typeof(IProvideValueTarget))
            {
                return new ProvideValueTarget(null, null);
            }
            throw new NotImplementedException();
        }
    }

    class ProvideValueTarget : IProvideValueTarget
    {
        private object _targetObject;
        private object _targetProperty;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ProvideValueTarget(
            object targetObject,
            object targetProperty
        )
        {
            _targetObject = targetObject;
            _targetProperty = targetProperty;
        }

        /// <summary>Gets the target object being reported.</summary>
        /// <returns>The target object being reported.</returns>
        public object TargetObject => _targetObject;

        /// <summary>Gets an identifier for the target property being reported.</summary>
        /// <returns>An identifier for the target property being reported.</returns>
        public object TargetProperty => _targetProperty;
    }

    public class AttachedContext : IDisposable
    {
        private ContextStack<InfoContext> contextStack;
        private object test;
        private InfoContext _infoContext;

        public AttachedContext(ContextStack<InfoContext> contextStack, object test)
        {
            this.contextStack = contextStack;
            this.test = test;
            _infoContext = new InfoContext( "", test );
            contextStack.Push( _infoContext );
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Assert.NotEmpty(contextStack);
            Assert.True(Object.ReferenceEquals(_infoContext, contextStack.First()));
            contextStack.Pop();
        }
    }
}
