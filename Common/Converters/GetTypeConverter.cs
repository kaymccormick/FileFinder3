﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// Common
// GetTypeConverter.cs
// 
// 2020-01-29-3:21 PM
// 
// ---
#endregion
using System ;
using System.Collections.Generic ;
using System.Globalization ;
using System.Reflection ;
using System.Windows.Data ;

namespace Common.Converters
{
	[ ValueConversion ( typeof ( Assembly ) , typeof ( IEnumerable < Type > ) ) ]
	public class GetTypeConverter : IValueConverter
	{
		/// <summary>Converts a value. </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		///     A converted value. If the method returns <see langword="null" />, the
		///     valid null value is used.
		/// </returns>
		public object Convert (
			object      value
		  , Type        targetType
		  , object      parameter
		  , CultureInfo culture
		)
		{
			return value.GetType ( ) ;
		}

		/// <summary>Converts a value. </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		///     A converted value. If the method returns <see langword="null" />, the
		///     valid null value is used.
		/// </returns>
		public object ConvertBack (
			object      value
		  , Type        targetType
		  , object      parameter
		  , CultureInfo culture
		)
		{
			throw new NotImplementedException ( ) ;
		}
	}
}