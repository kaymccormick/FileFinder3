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

namespace WpfApp1.Controls
{
	/// <summary>
	/// Interaction logic for Container.xaml
	/// </summary>
	public partial class Container : UserControl
	{
		public Container ()
		{
			InitializeComponent ( );
		}

		private void LoadInstance ( object sender , ExecutedRoutedEventArgs e ) { throw new NotImplementedException ( ) ; }
		private void Metadata ( object sender , ExecutedRoutedEventArgs e ) { throw new NotImplementedException ( ) ; }
		private void InstancesOnly_OnChecked ( object sender , RoutedEventArgs e ) { throw new NotImplementedException ( ) ; }
		private void InstancesOnly_OnUnchecked ( object sender , RoutedEventArgs e ) { throw new NotImplementedException ( ) ; }
	}
}
