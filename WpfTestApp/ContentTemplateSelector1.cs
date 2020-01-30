using System.Windows ;
using System.Windows.Controls ;

namespace WpfTestApp
{
	public class ContentTemplateSelector1 : DataTemplateSelector
	{
		/// <summary>When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.</summary>
		/// <param name="item">The data object for which to select the template.</param>
		/// <param name="container">The data-bound object.</param>
		/// <returns>Returns a <see cref="T:System.Windows.DataTemplate" /> or <see langword="null" />. The default value is <see langword="null" />.</returns>
		public override DataTemplate SelectTemplate ( object item 
			, DependencyObject container )
		{

			var dt = ( container as FrameworkElement ).TryFindResource ( item.GetType ( ) ) as
			                      DataTemplate ;
			return dt ;
		}
	}
}