using System.ComponentModel;
using System.Reflection;
using System.Windows;
using NLog;
using WpfApp1.Util ;
using WpfApp1Tests3.Utils;

namespace WpfApp1Tests3
{
	static internal class DumpHelper
	{
		private static readonly Logger Logger =
			LogManager.GetCurrentClassLogger();

		public static void DumpResource(
			ContextStack < InfoContext > context,
			object                       resource,
			InfoContext.Factory          servicesInfoContextFactory
		)
		{
			context.Push( servicesInfoContextFactory( "resource", resource ) );
			if ( resource is Style style )
			{
				Logger.Debug( $"TargetType = {style.TargetType}" );
				foreach ( var setter in style.Setters )
				{
					switch ( setter )
					{
						case Setter s:
							Logger.Debug( $"{context} : Setter" );
							DumpDependencyProperty( context, s.Property, servicesInfoContextFactory);
							Logger.Debug( $"TargetName = {s.TargetName}" );
							Logger.Debug( $"Value = {s.Value}" );
							DumpValue( context, s.Value, servicesInfoContextFactory );
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

		private static void DumpDependencyProperty(
			ContextStack < InfoContext > context,
			DependencyProperty           sProperty,
			InfoContext.Factory services1InfoContextFactory
		)
		{
			context.Push( services1InfoContextFactory( "DependencyProperty", sProperty ) );
			var prefix = context.ToString();
			Logger.Debug( $"DependencyProperty: {sProperty.Name}" );
			Logger.Debug( $"DependencyProperty.PropertyType: {sProperty.PropertyType}" );
			Logger.Debug( $"DependencyProperty.OwnerType: {sProperty.OwnerType}" );
		}

		private static void DumpValue(
			ContextStack < InfoContext > context,
			object                       sValue,
			InfoContext.Factory          servicesInfoContextFactory
		)
		{
			context.Push( servicesInfoContextFactory( "value", sValue ) );

			var prefix = context.ToString();
			switch ( sValue )
			{
				case DynamicResourceExtension d:
					Logger.Debug( $"Value Type {d.GetType()}" );
					Logger.Debug( $"Resource Key {d.ResourceKey}" );
					var provideValue = d.ProvideValue( new ServiceProviderProxy() );
					DumpProvidedValue( context, provideValue, servicesInfoContextFactory );

					Logger.Debug( $"ProvideValue is {provideValue}" );
					break;
				default:
					Logger.Debug( "Value: " );
					break;
			}

			context.Pop();
		}

		private static void DumpProvidedValue(
			ContextStack < InfoContext > context,
			object                       provideValue,
			InfoContext.Factory          services1InfoContextFactory
		)
		{
			var prefix = context.ToString();
			Logger.Debug( $"type of provided value is {provideValue.GetType()}" );
			var typeConverter = TypeDescriptor.GetConverter( provideValue );
			context.Push( services1InfoContextFactory( "provideValue", provideValue ) );
			if ( typeConverter.CanConvertTo( typeof(string) ) )
			{
				var convertTo = typeConverter.ConvertTo( provideValue, typeof(string) );
				Logger.Debug( $"converted to {convertTo}" );
			}

			foreach ( var p in provideValue.GetType().GetFields( BindingFlags.NonPublic | BindingFlags.Instance ) )
			{
				Logger.Debug( $"field {p.Name} = {p.GetValue( provideValue )}" );
			}

			foreach ( var p in provideValue.GetType().GetProperties( BindingFlags.NonPublic | BindingFlags.Instance ) )
			{
				Logger.Debug( $"property {p.Name} = {p.GetValue( provideValue )}" );
			}

			context.Pop();
		}
	}
}