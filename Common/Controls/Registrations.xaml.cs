using System.Windows ;
using System.Windows.Controls ;
using AppShared ;
using Autofac ;
using NLog ;

namespace Common.Controls
{
	/// <summary>
	///     Interaction	logic for Registrations.xaml
	/// </summary>
	public partial class Registrations : UserControl
	{
		public static readonly DependencyProperty
			LifetimeScopeProperty = App.LifetimeScopeProperty ;

		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		public Registrations ( )
		{
			InitializeComponent ( ) ;
//			AddHandler (  )

			AddHandler (
			            App.LifetimeScopeChangedEvent
			          , new RoutedPropertyChangedEventHandler < ILifetimeScope > ( Target )
			           ) ;
		}

		private void Target ( object sender , RoutedPropertyChangedEventArgs < ILifetimeScope > e )
		{
			// Logger.Debug ( $"LifetimeScopeChanged {sender} {e.NewValue}" ) ;
		}


		
	}
}