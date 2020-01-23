using System ;
using NLog ;

namespace WpfApp1.Interfaces
{
	public interface ILoggerTracker
	{
		void TrackLogger ( string loggerName , ILogger logger ) ;

		event LoggerRegisteredEventHandler LoggerRegistered ;

	}

	public delegate void LoggerRegisteredEventHandler ( object sender , LoggerEventArgs args ) ;

	public class LoggerEventArgs : EventArgs
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.EventArgs" />
		///     class.
		/// </summary>
		public LoggerEventArgs ( ILogger logger ) { Logger = logger ; }

		public ILogger Logger { get ; }
	}
}