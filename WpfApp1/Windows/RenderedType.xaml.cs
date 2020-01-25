using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.Windows
{
	/// <summary>
	/// Interaction logic for RenderedType.xaml
	/// </summary>
	public partial class RenderedType : UserControl
	{
		public RenderedType ()
		{
			InitializeComponent ( );
		}

		private void CommandBinding_OnCanExecute ( object sender , CanExecuteRoutedEventArgs e )
		{
			e.CanExecute = true ;
		}
	}
}
