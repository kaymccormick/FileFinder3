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
using AppShared.Types ;
using NLog ;

namespace WpfTestApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private static Logger Logger = NLog.LogManager.GetCurrentClassLogger ( ) ;
		public MainWindow ()
		{
			InitializeComponent ( );

			SetValue (
			          AppShared.App.LifetimeScopeProperty
			        , ( Application.Current as App ).MyLifetimeScope
			         ) ;
			var resolveServiceList = TryFindResource ( "r" ) as ResolveServiceList ;
			foreach ( var resolveService in resolveServiceList )
			{
				var valueSource = DependencyPropertyHelper.GetValueSource (
				                                                           resolveService
				                                                         , AppShared.App.LifetimeScopeProperty
				                                                          ) ;
				var lifetimeScope = AppShared.App.GetLifetimeScope ( resolveService ) ;
				Logger.Warn ( $"{resolveService.ServiceType} {lifetimeScope} {valueSource}" ) ;
				

			}
		}


	}
}
