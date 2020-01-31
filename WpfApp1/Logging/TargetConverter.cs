using System ;
using System.Collections ;
using System.ComponentModel ;
using System.Globalization ;
using System.Windows.Data ;
using NLog.Targets ;

namespace WpfApp1.Logging
{
	public class TargetConverter : TypeConverter , IValueConverter
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
		object IValueConverter.Convert (
			object      value
		  , Type        targetType
		  , object      parameter
		  , CultureInfo culture
		)
		{
			return Convert ( value , targetType , parameter , culture ) ;
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
		object IValueConverter.ConvertBack (
			object      value
		  , Type        targetType
		  , object      parameter
		  , CultureInfo culture
		)
		{
			return ConvertBack ( value , targetType , parameter , culture ) ;
		}

		/// <summary>
		///     Creates an instance of the type that this
		///     <see cref="T:System.ComponentModel.TypeConverter" /> is associated with,
		///     using the specified context, given a set of property values for the object.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <param name="propertyValues">
		///     An <see cref="T:System.Collections.IDictionary" />
		///     of new property values.
		/// </param>
		/// <returns>
		///     An <see cref="T:System.Object" /> representing the given
		///     <see cref="T:System.Collections.IDictionary" />, or <see langword="null" />
		///     if the object cannot be created. This method always returns
		///     <see langword="null" />.
		/// </returns>
		public override object CreateInstance (
			ITypeDescriptorContext context
		  , IDictionary            propertyValues
		)
		{
			return base.CreateInstance ( context , propertyValues ) ;
		}

		/// <summary>
		///     Returns whether changing a value on this object requires a call to
		///     <see
		///         cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" />
		///     to create a new value, using the specified context.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <returns>
		///     <see langword="true" /> if changing a property on this object requires a
		///     call to
		///     <see
		///         cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" />
		///     to create a new value; otherwise, <see langword="false" />.
		/// </returns>
		public override bool GetCreateInstanceSupported ( ITypeDescriptorContext context )
		{
			return base.GetCreateInstanceSupported ( context ) ;
		}

		/// <summary>
		///     Returns a collection of properties for the type of array specified by
		///     the value parameter, using the specified context and attributes.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <param name="value">
		///     An <see cref="T:System.Object" /> that specifies the type
		///     of array for which to get properties.
		/// </param>
		/// <param name="attributes">
		///     An array of type <see cref="T:System.Attribute" />
		///     that is used as a filter.
		/// </param>
		/// <returns>
		///     A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" />
		///     with the properties that are exposed for this data type, or
		///     <see langword="null" /> if there are no properties.
		/// </returns>
		public override PropertyDescriptorCollection GetProperties (
			ITypeDescriptorContext context
		  , object                 value
		  , Attribute[]            attributes
		)
		{
			return base.GetProperties ( context , value , attributes ) ;
		}

		/// <summary>
		///     Returns whether this object supports properties, using the specified
		///     context.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <returns>
		///     <see langword="true" /> if
		///     <see
		///         cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" />
		///     should be called to find the properties of this object; otherwise,
		///     <see langword="false" />.
		/// </returns>
		public override bool GetPropertiesSupported ( ITypeDescriptorContext context )
		{
			return base.GetPropertiesSupported ( context ) ;
		}

		/// <summary>
		///     Returns a collection of standard values for the data type this type
		///     converter is designed for when provided with a format context.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context that can be used to extract additional information about
		///     the environment from which this converter is invoked. This parameter or
		///     properties of this parameter can be <see langword="null" />.
		/// </param>
		/// <returns>
		///     A
		///     <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" />
		///     that holds a standard set of valid values, or <see langword="null" /> if
		///     the data type does not support a standard set of values.
		/// </returns>
		public override StandardValuesCollection GetStandardValues (
			ITypeDescriptorContext context
		)
		{
			return base.GetStandardValues ( context ) ;
		}

		/// <summary>
		///     Returns whether the collection of standard values returned from
		///     <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is
		///     an exclusive list of possible values, using the specified context.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <returns>
		///     <see langword="true" /> if the
		///     <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" />
		///     returned from
		///     <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is
		///     an exhaustive list of possible values; <see langword="false" /> if other
		///     values are possible.
		/// </returns>
		public override bool GetStandardValuesExclusive ( ITypeDescriptorContext context )
		{
			return base.GetStandardValuesExclusive ( context ) ;
		}

		/// <summary>
		///     Returns whether this object supports a standard set of values that can
		///     be picked from a list, using the specified context.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <returns>
		///     <see langword="true" /> if
		///     <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" />
		///     should be called to find a common set of values the object supports;
		///     otherwise, <see langword="false" />.
		/// </returns>
		public override bool GetStandardValuesSupported ( ITypeDescriptorContext context )
		{
			return base.GetStandardValuesSupported ( context ) ;
		}

		/// <summary>
		///     Returns whether this converter can convert an object of the given type
		///     to the type of this converter, using the specified context.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <param name="sourceType">
		///     A <see cref="T:System.Type" /> that represents the
		///     type you want to convert from.
		/// </param>
		/// <returns>
		///     <see langword="true" /> if this converter can perform the conversion;
		///     otherwise, <see langword="false" />.
		/// </returns>
		public override bool CanConvertFrom ( ITypeDescriptorContext context , Type sourceType )
		{
			return base.CanConvertFrom ( context , sourceType ) ;
		}

		/// <summary>
		///     Returns whether this converter can convert the object to the specified
		///     type, using the specified context.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <param name="destinationType">
		///     A <see cref="T:System.Type" /> that represents
		///     the type you want to convert to.
		/// </param>
		/// <returns>
		///     <see langword="true" /> if this converter can perform the conversion;
		///     otherwise, <see langword="false" />.
		/// </returns>
		public override bool CanConvertTo ( ITypeDescriptorContext context , Type destinationType )
		{
			if ( destinationType == typeof ( string ) )
			{
				return true ;
			}

			return base.CanConvertTo ( context , destinationType ) ;
		}

		/// <summary>
		///     Converts the given object to the type of this converter, using the
		///     specified context and culture information.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <param name="culture">
		///     The <see cref="T:System.Globalization.CultureInfo" /> to
		///     use as the current culture.
		/// </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///     The conversion cannot be
		///     performed.
		/// </exception>
		public override object ConvertFrom (
			ITypeDescriptorContext context
		  , CultureInfo            culture
		  , object                 value
		)
		{
			return base.ConvertFrom ( context , culture , value ) ;
		}

		/// <summary>
		///     Converts the given value object to the specified type, using the
		///     specified context and culture information.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <param name="culture">
		///     A <see cref="T:System.Globalization.CultureInfo" />. If
		///     <see langword="null" /> is passed, the current culture is assumed.
		/// </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">
		///     The <see cref="T:System.Type" /> to convert the
		///     <paramref name="value" /> parameter to.
		/// </param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///     The
		///     <paramref name="destinationType" /> parameter is <see langword="null" />.
		/// </exception>
		/// <exception cref="T:System.NotSupportedException">
		///     The conversion cannot be
		///     performed.
		/// </exception>
		public override object ConvertTo (
			ITypeDescriptorContext context
		  , CultureInfo            culture
		  , object                 value
		  , Type                   destinationType
		)
		{
			try
			{
				var target = value as Target ;
				if ( target is NetworkTarget n )
				{
					return n.Address ;
				}
			}
			catch ( Exception )
			{
			}

			return base.ConvertTo ( context , culture , value , destinationType ) ;
		}

		/// <summary>
		///     Returns whether the given value object is valid for this type and for
		///     the specified context.
		/// </summary>
		/// <param name="context">
		///     An
		///     <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides
		///     a format context.
		/// </param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity.</param>
		/// <returns>
		///     <see langword="true" /> if the specified value is valid for this object;
		///     otherwise, <see langword="false" />.
		/// </returns>
		public override bool IsValid ( ITypeDescriptorContext context , object value )
		{
			return base.IsValid ( context , value ) ;
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString ( ) { return base.ToString ( ) ; }

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
			// var typeDescriptionProvider = new TypeDescriptionProvider();
			// var customTypeDescriptor = typeDescriptionProvider.GetTypeDescriptor( value );
			if ( ( string ) parameter == "GetType" )
			{
				return ConvertTo ( value.GetType ( ) , targetType ) ;
			}

			return ConvertTo ( value , targetType ) ;
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