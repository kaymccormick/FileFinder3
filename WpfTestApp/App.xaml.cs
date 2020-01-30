using System.Collections.Generic;
using System.Diagnostics ;
using System.Windows;
using AppShared.Interfaces ;
using Autofac ;
using Common.Tracing ;
using Common.Utils ;
using NLog ;

namespace WpfTestApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application, IHaveLifetimeScope
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;
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
			if ( DoTracing )
			{
				InitializeTracing ( ) ;
			}
		}

		public bool DoTracing
			{ get ; set ; }

		private void InitializeTracing ( )
		{
			PresentationTraceSources.Refresh ( ) ;
			var nLogTraceListener = new NLogTraceListener ( ) ;
			List <TraceSource> sources = new List < TraceSource > ();

			sources.Add ( PresentationTraceSources.ResourceDictionarySource ) ;
			sources.Add ( PresentationTraceSources.DependencyPropertySource ) ;
			sources.Add ( PresentationTraceSources.NameScopeSource ) ;
			sources.Add ( PresentationTraceSources.MarkupSource ) ;
			sources.Add ( PresentationTraceSources.DataBindingSource ) ;
			sources.Add ( PresentationTraceSources.DependencyPropertySource ) ;

			var s1 = PresentationTraceSources.ResourceDictionarySource ;
			var source = PresentationTraceSources.DataBindingSource ;
			nLogTraceListener.DefaultLogLevel = LogLevel.Debug ;
			// nLogTraceListener.ForceLogLevel = LogLevel.Trace ;
			//nLogTraceListener.LogFactory      = AppContainer.Resolve < LogFactory > ( ) ;
			nLogTraceListener.AutoLoggerName = false ;
			//nLogTraceListener.
			sources.ForEach (
			                 traceSource => {
				                 traceSource.Switch.Level = SourceLevels.All ;
				                 traceSource.Listeners.Add ( nLogTraceListener ) ;
				                 traceSource.Listeners.Add ( new AppTraceLisener2 ( ) ) ;
			                 }
			                ) ;
		}

		public ILifetimeScope MyLifetimeScope
			{ get ; set ; }

		private void Target ( object sender , RoutedEventArgs e )
		{
			Window w = sender as Window ;
			Logger.Warn  ($"Setting LifetimeScope to {MyLifetimeScope}");

			w.SetValue(AppShared.App.LifetimeScopeProperty, MyLifetimeScope);
		}

		public ILifetimeScope LifetimeScope
		{
			get => MyLifetimeScope ;
			set => MyLifetimeScope = value ;
		}
	}
}
