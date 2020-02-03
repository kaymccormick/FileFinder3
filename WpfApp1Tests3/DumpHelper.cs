using System.ComponentModel ;
using System.Reflection ;
using System.Windows ;
using AppShared ;
using AppShared.Infos ;
using NLog ;

namespace WpfApp1Tests3
{
	internal static class DumpHelper
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		public static void DumpResource (
			ContextStack < InfoContext > context
		  , object                       resource
		  , InfoContext.Factory          servicesInfoContextFactory
		)
		{
			context.Push ( servicesInfoContextFactory ( "resource" , resource ) ) ;
			if ( resource is Style style )
			{
				Logger.Trace ( $"TargetType = {style.TargetType}" ) ;
				foreach ( var setter in style.Setters )
				{
					switch ( setter )
					{
						case Setter s :
							Logger.Trace ( $"{context} : Setter" ) ;
							DumpDependencyProperty (
							                        context
							                      , s.Property
							                      , servicesInfoContextFactory
							                       ) ;
							Logger.Trace ( $"TargetName = {s.TargetName}" ) ;
							Logger.Trace ( $"Value = {s.Value}" ) ;
							DumpValue ( context , s.Value , servicesInfoContextFactory ) ;
							break ;
						case EventSetter eventSetter :
							Logger.Trace (
							              $"{context} : EventSetter.Event = {eventSetter.Event}"
							             ) ;
							Logger.Trace (
							              $"{context} : HandledEventsToo = {eventSetter.HandledEventsToo}"
							             ) ;
							Logger.Trace ( $"{context} : Method {eventSetter.Handler.Method}" ) ;
							Logger.Trace ( $"{context} : Target {eventSetter.Handler.Target}" ) ;
							break ;
					}
				}
			}

			context.Pop ( ) ;
		}

		private static void DumpDependencyProperty (
			ContextStack < InfoContext > context
		  , DependencyProperty           sProperty
		  , InfoContext.Factory          services1InfoContextFactory
		)
		{
			context.Push ( services1InfoContextFactory ( "DependencyProperty" , sProperty ) ) ;
			var prefix = context.ToString ( ) ;
			Logger.Trace ( $"DependencyProperty: {sProperty.Name}" ) ;
			Logger.Trace ( $"DependencyProperty.PropertyType: {sProperty.PropertyType}" ) ;
			Logger.Trace ( $"DependencyProperty.OwnerType: {sProperty.OwnerType}" ) ;
		}

		private static void DumpValue (
			ContextStack < InfoContext > context
		  , object                       sValue
		  , InfoContext.Factory          servicesInfoContextFactory
		)
		{
			context.Push ( servicesInfoContextFactory ( "value" , sValue ) ) ;

			var prefix = context.ToString ( ) ;
			switch ( sValue )
			{
				case DynamicResourceExtension d :
					Logger.Trace ( $"Value Type {d.GetType ( )}" ) ;
					Logger.Trace ( $"Resource Key {d.ResourceKey}" ) ;
					var provideValue = d.ProvideValue ( new ServiceProviderProxy ( ) ) ;
					DumpProvidedValue ( context , provideValue , servicesInfoContextFactory ) ;

					Logger.Trace ( $"ProvideValue is {provideValue}" ) ;
					break ;
				default :
					Logger.Trace ( "Value: " ) ;
					break ;
			}

			context.Pop ( ) ;
		}

		private static void DumpProvidedValue (
			ContextStack < InfoContext > context
		  , object                       provideValue
		  , InfoContext.Factory          services1InfoContextFactory
		)
		{
			var prefix = context.ToString ( ) ;
			Logger.Trace ( $"type of provided value is {provideValue.GetType ( )}" ) ;
			var typeConverter = TypeDescriptor.GetConverter ( provideValue ) ;
			context.Push ( services1InfoContextFactory ( "provideValue" , provideValue ) ) ;
			if ( typeConverter.CanConvertTo ( typeof ( string ) ) )
			{
				var convertTo = typeConverter.ConvertTo ( provideValue , typeof ( string ) ) ;
				Logger.Trace ( $"converted to {convertTo}" ) ;
			}

			foreach ( var p in provideValue
			                  .GetType ( )
			                  .GetFields ( BindingFlags.NonPublic | BindingFlags.Instance ) )
			{
				Logger.Trace ( $"field {p.Name} = {p.GetValue ( provideValue )}" ) ;
			}

			foreach ( var p in provideValue
			                  .GetType ( )
			                  .GetProperties ( BindingFlags.NonPublic | BindingFlags.Instance ) )
			{
				Logger.Trace ( $"property {p.Name} = {p.GetValue ( provideValue )}" ) ;
			}

			context.Pop ( ) ;
		}
	}
}