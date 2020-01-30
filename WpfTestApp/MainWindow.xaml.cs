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
using AppShared ;
using AppShared.Types ;
using Common.Controls ;
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
			try
			{
				var resolveServiceList = TryFindResource ( "resolveServices" ) as ResolveServiceList ;
				foreach ( var resolveService in resolveServiceList )
				{
					var valueSource = DependencyPropertyHelper.GetValueSource (
					                                                           resolveService
					                                                         , AppShared
					                                                          .App
					                                                          .LifetimeScopeProperty
					                                                          ) ;
					var lifetimeScope = AppShared.App.GetLifetimeScope ( resolveService ) ;
					Logger.Warn ( $"{resolveService.ServiceType} {lifetimeScope} {valueSource}" ) ;


				}
			}
			catch ( Exception ex )
			{

			}
		}


		public object TypeFrame { get { return frame ; } }

		private void Frame_OnNavigationFailed ( object sender , NavigationFailedEventArgs e )
		{
			var msg = $"{e.Exception.Message}; {e.ExtraData}" ;
			Logger.Error ( $"Nav failed: {msg}" ) ;
		}

		private void Frame_OnNavigating ( object sender , NavigatingCancelEventArgs e )
		{
			Logger.Error ( e.ContentStateToSave ) ;
		}

		private void VisitType ( object sender , ExecutedRoutedEventArgs e )
		{
			var eParameter = e.Parameter as Type ;
			if ( eParameter != null )
			{
				var typeControl2 = new TypeControl2() ;
				typeControl2.SetValue(AppShared.App.RenderedTypeProperty, eParameter);
				var navigate = frame.Navigate(typeControl2) ;
			}
		}

		private void MainWindow_OnInitialized ( object sender , EventArgs e )
		{
			AssemblyList list = TryFindResource("AssemblyList") as AssemblyList;
			foreach ( var assembly in AppDomain.CurrentDomain.GetAssemblies ( ) )
			{
				list.Add ( assembly ) ;
			}
		}
	}
}
