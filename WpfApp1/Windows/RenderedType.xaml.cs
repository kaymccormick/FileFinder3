using System.Windows.Controls ;
using System.Windows.Input ;

namespace WpfApp1.Windows
{
	/// <summary>
	///     Interaction logic for RenderedType.xaml
	/// </summary>
	public partial class RenderedType : UserControl
	{
		public RenderedType ( ) { InitializeComponent ( ) ; }

		private void CommandBinding_OnCanExecute ( object sender , CanExecuteRoutedEventArgs e )
		{
			e.CanExecute = true ;
		}
	}
}