using System.Collections.Concurrent ;
using AppShared.Interfaces ;
using NLog ;

namespace WpfApp1.Logging
{
	public class LoggerTracker  : ILoggerTracker
	{
		private static ILogger _Logger = LogManager.GetCurrentClassLogger ( ) ;
		public ILogger Logger { get { return LoggerTracker._Logger ; } }
		ConcurrentDictionary <string, ILogger> loggers = new ConcurrentDictionary < string , ILogger >();
		public void TrackLogger ( string loggerName , ILogger logger )
		{
			ILogger existingLogger = null ;
			// race condition
			if ( loggers.TryGetValue ( loggerName , out existingLogger ) )
			{
				Logger.Debug ( $"logger {loggerName} exists already." ) ;
			}
			else
			{
				loggers.TryAdd ( loggerName , logger ) ;
				OnLoggerRegistered ( new LoggerEventArgs ( logger ) ) ;
			}
		} 

		protected virtual void OnLoggerRegistered ( LoggerEventArgs args )
		{
			LoggerRegisteredEventHandler handler = LoggerRegistered ;
			handler?.Invoke ( this , args ) ;
		}

		public event LoggerRegisteredEventHandler LoggerRegistered ;
	}
}