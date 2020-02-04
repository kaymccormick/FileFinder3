using System ;
using System.Windows.Markup ;
using System.Xaml ;
using Common.Logging ;
using Logging ;
using NLog ;
using LogManager = NLog.LogManager ;

namespace WpfApp1.Xaml
{
	[ MarkupExtensionReturnType ( typeof ( AppLogger ) ) ]
	internal class AppLoggerExtension : MarkupExtension
	{
		/// <summary>
		///     Initializes a new instance of a class derived from
		///     <see cref="T:System.Windows.Markup.MarkupExtension" />.
		/// </summary>
		public AppLoggerExtension ( )
		{
			AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ;
			Logger = LogManager.GetCurrentClassLogger ( ) ;
		}

		/// <summary>
		///     Initializes a new instance of a class derived from
		///     <see cref="T:System.Windows.Markup.MarkupExtension" />.
		/// </summary>
		public AppLoggerExtension ( string arg ) : this ( )
		{
			Arg = arg ;
			LogManager.GetCurrentClassLogger ( ).Info ( $"{arg}" ) ;
		}

		public ILogger Logger { get ; set ; }

		public string Arg { get ; set ; }

		/// <summary>
		///     When implemented in a derived class, returns an object that is
		///     provided as the value of the target property for this markup extension.
		/// </summary>
		/// <param name="serviceProvider">
		///     A service provider helper that can provide
		///     services for the markup extension.
		/// </param>
		/// <returns>
		///     The object value to set on the property where the extension is
		///     applied.
		/// </returns>
		public override object ProvideValue ( IServiceProvider serviceProvider )
		{
			var service = serviceProvider.GetService ( typeof ( IProvideValueTarget ) ) ;
			var provideValueTarget = service as IProvideValueTarget ;
			Console.WriteLine ( provideValueTarget.TargetObject ) ;

			var service2 = serviceProvider.GetService ( typeof ( IRootObjectProvider ) ) ;
			var provide = service as IRootObjectProvider ;

			Console.WriteLine ( provide.RootObject ) ;
			return new AppLogger ( LogManager.GetCurrentClassLogger ( ) ) ;
		}
	}
}