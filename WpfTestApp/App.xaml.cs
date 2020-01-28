using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics ;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac ;
using Common ;
using Common.Utils ;
using NLog ;

namespace WpfTestApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly static Logger Logger = LogManager.GetCurrentClassLogger ( ) ;
		/// <summary>Raises the <see cref="E:System.Windows.Application.Startup" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
		protected override void OnStartup ( StartupEventArgs e )
		{
			Initialize();
			base.OnStartup ( e ) ;

		}

		public void Initialize ( )
		{
			IContainer container ;
			MyLifetimeScope = ContainerHelper.SetupContainer ( out container ) ;

			InitializeTracing ( ) ;
		}

		private void InitializeTracing ( )
		{
			PresentationTraceSources.Refresh ( ) ;
			var nLogTraceListener = new NLogTraceListener ( ) ;
			var source = PresentationTraceSources.DataBindingSource ;
			nLogTraceListener.DefaultLogLevel = LogLevel.Debug ;
			nLogTraceListener.ForceLogLevel   = LogLevel.Warn ;
			//nLogTraceListener.LogFactory      = AppContainer.Resolve < LogFactory > ( ) ;
			nLogTraceListener.AutoLoggerName = false ;
			//nLogTraceListener.
			source.Switch.Level = SourceLevels.All ;
			#if resolve
			var foo = AppContainer.Resolve < IEnumerable < TraceListener > > ( ) ;
			foreach ( var tl in foo )
			{
				source.Listeners.Add ( tl ) ;
			}
#endif

			//routedEventSource.Listeners.Add ( new AppTraceLisener ( ) ) ;
			source.Listeners.Add ( nLogTraceListener ) ;

		}

		public ILifetimeScope MyLifetimeScope
			{ get ; set ; }

		private void Target ( object sender , RoutedEventArgs e )
		{
			Window w = ( sender as Window ) ;
			Logger.Warn  ($"Setting LifetimeScope to {MyLifetimeScope}");

			w.SetValue(AppShared.App.LifetimeScopeProperty, MyLifetimeScope);
		}
	}
}
