#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// RegistrationConverter2.cs
// 
// 2020-01-26-10:09 AM
// 
// ---
#endregion
using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.Globalization ;
using System.Linq ;
using System.Windows.Data ;
using AppShared ;
using AppShared.Services ;
using Autofac.Core ;
using Castle.DynamicProxy.Internal ;
using NLog ;

namespace Common.Converters
{
	public class RegistrationConverter2 : IValueConverter
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger ( ) ;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public RegistrationConverter2 ( ) { }

		/// <summary>Converts a value. </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
		public object Convert (
			object      value
		  , Type        targetType
		  , object      parameter
		  , CultureInfo culture
		)
		{
			Logger.Info ( $"{value.GetType ( )} {targetType} param={parameter};" ) ;
			var icomp = value as IComponentRegistration ;
			var comp = value as ComponentRegistration ;
			if ( comp == null && icomp == null )
			{
				Logger.Warn ( $"Wrong type {value.GetType()} {String.Join(", ", value.GetType().GetAllInterfaces().Select(type => type.Name))}" ) ;
				if ( targetType == typeof ( string ) )
				{
					return "" ;
				}
				else if ( targetType.IsSubclassOf ( typeof ( IEnumerable ) ) )
				{
					return new object[ 0 ] ;
				}

				return null ;
			}

			var x = comp.Instances != null ? comp.Instances : new List < InstanceInfo > ( ) ;
			Logger.Debug ( $"Using list of {string.Join ( ", " , x )}" ) ;

			if ( parameter is string s )
			{
				return x.Count ;
			}

			if ( typeof ( IEnumerable ).IsAssignableFrom ( targetType ) )
			{
				return x ;
			}


			return null ;
		}


		/// <summary>Converts a value. </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value. If the method returns <see //langword="null" />, the valid null value is used.</returns>
		public object ConvertBack (
			object      value
		  , Type        targetType
		  , object      parameter
		  , CultureInfo culture
		)
		{
			return - 1 ;
		}
	}
}