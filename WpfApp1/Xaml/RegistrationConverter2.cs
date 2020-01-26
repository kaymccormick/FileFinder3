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
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using DynamicData.Kernel ;
using WpfApp1.Windows ;

namespace WpfApp1.Xaml
{
	internal class RegistrationConverter2 : IValueConverter
	{
		private readonly ILifetimeScope    _appContainer ;
		private readonly IObjectIdProvider _provider ;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public RegistrationConverter2 ( ) {
		}

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
			var comp = value as IComponentRegistration ;
			if ( comp == null )
			{
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

			ComponentRegistration myComp = comp as ComponentRegistration ;
			IList < InstanceInfo > x = myComp != null
				                           ? myComp.Instances
				                           : new List < InstanceInfo > ( ) ;
			if ( parameter is string s ) return x.Count ;
			if ( targetType.IsAssignableTo < IEnumerable > ( ) )
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
		/// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
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