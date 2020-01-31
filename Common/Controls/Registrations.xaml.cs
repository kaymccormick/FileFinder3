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


		/// <summary>
		///     Invoked whenever the effective value of any dependency property on this
		///     <see cref="T:System.Windows.FrameworkElement" /> has been updated. The
		///     specific dependency property that changed is reported in the arguments
		///     parameter. Overrides
		///     <see
		///         cref="M:System.Windows.DependencyObject.OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs)" />
		///     .
		/// </summary>
		/// <param name="e">
		///     The event data that describes the property that changed, as
		///     well as old and new values.
		/// </param>
		protected override void OnPropertyChanged ( DependencyPropertyChangedEventArgs e )
		{
			base.OnPropertyChanged ( e ) ;
			// Logger.Debug ( $"{e.Property.Name} = {e.NewValue}" ) ;
		}
	}
}