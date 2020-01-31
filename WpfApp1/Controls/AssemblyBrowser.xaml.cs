using System ;
using System.Windows ;
using System.Windows.Controls ;
using System.Windows.Input ;
using AppShared ;
using NLog ;

namespace WpfApp1.Controls
{
	/// <summary>
	///     Interaction logic for AssemblyBrowser.xaml
	/// </summary>
	public partial class AssemblyBrowser : UserControl
	{
		public static readonly DependencyProperty AssemblyListPrroperty = App.AssemblyListProperty ;

		private static Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		public AssemblyBrowser ( ) { InitializeComponent ( ) ; }

		private void LoadAssemblyList ( object sender , ExecutedRoutedEventArgs e )
		{
			throw new NotImplementedException ( ) ;
		}

		private void CanLoadAssemblyList ( object sender , CanExecuteRoutedEventArgs e ) { }
	}
}