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
using Xunit;

namespace WpfApp1Tests3
{
    [Collection("WpfApp")]
    public class WpfTests : IClassFixture <ContainerFixture>
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public GenericApplicationFixture fixture;
        private readonly ContainerFixture _containerFixture;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WpfTests(
            GenericApplicationFixture fixture,
            ContainerFixture containerFixture
        )
        {
            this.fixture = fixture;
            _containerFixture = containerFixture;
        }

        [Fact]
        public void Test1()
        {
            var candidateConfigFilePaths = LogManager.LogFactory.GetCandidateConfigFilePaths();
            foreach(var q in candidateConfigFilePaths)
            {
                Logger.Debug($"{q}");
            }
            var loggingConfiguration = LogManager.Configuration;
            var fieldInfo = loggingConfiguration.GetType().GetField( "_originalFileName", BindingFlags.NonPublic | BindingFlags.Instance);
            if ( fieldInfo != null )
            {
                Logger.Debug( $"Original NLOG configuration filename is {fieldInfo.GetValue( loggingConfiguration )}" );
            }
            Logger.Debug( $"{loggingConfiguration}" );
            using (var scope = _containerFixture.LifetimeScope.BeginLifetimeScope())
            {
                var menuItemList = scope.Resolve < IMenuItemList >();
                Assert.NotNull(menuItemList);
                Assert.NotEmpty(menuItemList);
                fixture.MyApp.Resources["MyMenuItemList"] = menuItemList;
                var found = fixture.MyApp.FindResource( "MyMenuItemList" );
                Assert.NotNull( found );
                var x = String.Join( ", ", menuItemList.First().Children );
                Logger.Debug( $"found {found}, {x}" );
                var uri = new Uri( fixture.BasePackUri, "Resources/MenuResources.xaml" );
                Logger.Debug($"{uri}"  );

                var stream = Application.GetResourceStream( uri );
                Logger.Info(stream.ContentType  );
                var baml2006Reader = new Baml2006Reader( stream.Stream );
                
                
                var o =XamlReader.Load( baml2006Reader );
                ///var o = Application.LoadComponent( uri );
                var menuResources = o as ResourceDictionary;
                foreach ( var q in menuResources.Keys )
                {
                    var resource = menuResources[q];
                    Logger.Debug($"{q}: {resource}"  );
                    DumpResource( resource );
                }


            }
        }

        private void DumpResource(
            object resource
        )
        {
            if ( resource is Style style )
            {
                Logger.Debug($"TargetType = {style.TargetType}"  );
                foreach ( var setter in style.Setters )
                {
                    switch ( setter )
                    {
                        case Setter s:
                            Logger.Debug( $"Setter" );
                            DumpDependencyProperty( s.Property );
                        Logger.Debug($"TargetName = {s.TargetName}" );
                            Logger.Debug( $"Value = {s.Value}" );
                            DumpValue(  s.Value );
                            break;
                        case EventSetter eventSetter:
                            Logger.Debug($"EventSetter.Event = {eventSetter.Event}"  );
                            Logger.Debug($"HandledEventsToo = {eventSetter.HandledEventsToo}"  );
                            Logger.Debug($"Method {eventSetter.Handler.Method}");
                            Logger.Debug($"Target {eventSetter.Handler.Target}");
                            break;
                    }
                }
            }

        }

        private void DumpValue(
            object sValue
        )
        {
            switch ( sValue )
            {
                case DynamicResourceExtension d:
                    Logger.Debug($"Value Type {d.GetType()}"  );
                    Logger.Debug($"Resource Key {d.ResourceKey}");
                    var provideValue = d.ProvideValue(new ServiceProviderProxy());
                    DumpProvidedValue( provideValue );
                    
                    Logger.Debug($"ProvideValue is {provideValue}");
                    break;
                default:
                    Logger.Debug("Value: ");
                    break;
            }
            
        }

        private void DumpProvidedValue(
            object provideValue
        )
        {
            Logger.Debug($"type of provided value is {provideValue.GetType()}"  );
            var typeConverter = TypeDescriptor.GetConverter( provideValue );
            
            if ( typeConverter.CanConvertTo( typeof(string) ) )
            {
                var convertTo = typeConverter.ConvertTo( provideValue, typeof(string) );
                Logger.Debug($"converted to {convertTo}"  );
            }

            foreach (var p in provideValue.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Logger.Debug($"field {p.Name} = {p.GetValue(provideValue)}");
            }
            foreach (var p in provideValue.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Logger.Debug($"property {p.Name} = {p.GetValue(provideValue)}");
            }

        }

        private void DumpDependencyProperty(
            DependencyProperty sProperty
        )
        {
            Logger.Debug($"DependencyProperty: {sProperty.Name}");
            Logger.Debug($"DependencyProperty.PropertyType: {sProperty.PropertyType}");
            Logger.Debug($"DependencyProperty.OwnerType: {sProperty.OwnerType}");
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
            Logger.Debug($"{nameof(GetService)} {serviceType}"  );
            if ( serviceType == typeof(IProvideValueTarget) )
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
}
