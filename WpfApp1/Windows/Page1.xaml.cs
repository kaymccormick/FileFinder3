using System;
using System.Collections.Generic;
using System.Collections.ObjectModel ;
using System.Linq;
using System.Reflection ;
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
using Common.Logging ;
using DynamicData ;
using NLog ;

namespace WpfApp1.Windows
{
	/// <summary>
	/// Interaction logic for Page1.xaml
	/// </summary>
	public partial class Page1 : Page
	{
		private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger ( ) ;
		public ObservableCollection <Assembly> AssemblyList = new ObservableCollection < Assembly > (AppDomain.CurrentDomain.GetAssemblies());
		public Page1 ()
		{
			InitializeComponent ( );
			AssemblyList.Clear();
			AssemblyList.AddRange ( AppDomain.CurrentDomain.GetAssemblies ( ) ) ;
			foreach ( var assembly in AssemblyList )
			{
				Logger.Debug ( assembly ) ;
			}

			//var xx = Resources[ "assemblySource" ] as CollectionViewSource ;
			CollectionViewSource.GetDefaultView(AssemblyList).Refresh();

			//typeBox.Items.Add ( typeof ( List < String > ) ) ;
		}
	}
}
