using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xaml;
using Common ;
using Common.Logging ;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using WpfApp1.Logging;
using LogManager = NLog.LogManager ;

namespace WpfApp1.Xaml
{
	[MarkupExtensionReturnType(typeof(AppLogger))]
	class AppLoggerExtension : MarkupExtension
	{
		public ILogger Logger { get ; set ; }
		/// <summary>Initializes a new instance of a class derived from <see cref="T:System.Windows.Markup.MarkupExtension" />.</summary>
		public AppLoggerExtension()
		{
			AppLoggingConfigHelper.EnsureLoggingConfigured( );
			Logger = LogManager.GetCurrentClassLogger ( ) ;
		}

		public string Arg { get; set; }

		/// <summary>Initializes a new instance of a class derived from <see cref="T:System.Windows.Markup.MarkupExtension" />.</summary>
		public AppLoggerExtension(
			string arg
		) : this()
		{
			Arg = arg;
			LogManager.GetCurrentClassLogger().Info($"{arg}");
		}

		/// <summary>When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension.</summary>
		/// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
		/// <returns>The object value to set on the property where the extension is applied.</returns>
		public override object ProvideValue(
			IServiceProvider serviceProvider
		)
		{
			var service = serviceProvider.GetService(typeof(IProvideValueTarget));
			var provideValueTarget = service as IProvideValueTarget;
			Console.WriteLine(provideValueTarget.TargetObject);

			var service2 = serviceProvider.GetService(typeof(IRootObjectProvider));
			var provide= service as IRootObjectProvider;

			Console.WriteLine(provide.RootObject);
			return new AppLogger( LogManager.GetCurrentClassLogger() );
		}
	}
}
