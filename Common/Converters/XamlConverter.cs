using System ;
using System.Globalization ;
using System.IO ;
using System.Windows ;
using System.Windows.Data ;
using System.Xaml ;

namespace Common.Converters
{
	public class XamlConverter : IValueConverter
	{
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
			try
			{
				DataTemplate d = value as DataTemplate ;
				d.LoadContent ( ) ;
				var stringWriter = new StringWriter ( ) ;

				XamlServices.Save ( stringWriter , d.Template ) ;
				return stringWriter.ToString ( ) ;
			} catch (Exception ex)
			{
				return ex.Message ;
			}
		}

		/// <summary>Converts a value. </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
		public object ConvertBack ( object value , Type targetType , object parameter , CultureInfo culture ) { throw new NotImplementedException ( ) ; }
	}
}