using System;
using System.Collections.Generic;
using System.Globalization ;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls ;
using System.Windows.Data ;
using Autofac ;
using WpfApp1.Commands ;

namespace WpfApp1.Xaml
{
	public class InstanceRegistrationConverter : IValueConverter
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
			InstanceRegistration x = value as InstanceRegistration;
			if ( x.Type.IsGenericType && x.Type.GetGenericTypeDefinition() == typeof(Lazy <object>).GetGenericTypeDefinition()) 
			{
				var r = new List < object > ( ) ;
				r.Add (
				       new Button ( )
				       {
					       Content          = "Load"
					     , Command          = MyAppCommands.Load
					     , CommandParameter = x.Instance
				       }
				      ) ;
				return r ;
			}

			return new object[ 0 ] ;
			throw new NotImplementedException ( ) ;
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
