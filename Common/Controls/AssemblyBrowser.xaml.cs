using System ;
using System.Windows ;
using System.Windows.Controls ;
using System.Windows.Input ;
using AppShared ;

namespace Common.Controls
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
