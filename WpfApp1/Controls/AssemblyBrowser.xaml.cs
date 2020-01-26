using System;
using System.Collections.Generic;
using System.Diagnostics ;
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
using AppShared ;
using WpfApp1.Xaml ;

namespace WpfApp1.Controls
{
	/// <summary>
	/// Interaction logic for AssemblyBrowser.xaml
	/// </summary>
	public partial class AssemblyBrowser : UserControl
	{
		public static readonly DependencyProperty AssemblyListPrroperty =
			App.AssemblyListProperty ;
		private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
		public AssemblyBrowser ()
		{
			InitializeComponent ( );
		}

		private void LoadAssemblyList ( object sender , ExecutedRoutedEventArgs e ) { throw new NotImplementedException ( ) ; }
		private void CanLoadAssemblyList ( object sender , CanExecuteRoutedEventArgs e ) {  }
	}
}
