using System.Collections.Concurrent ;
using AppShared.Interfaces ;
using NLog ;

namespace Common.Logging
{
	public class LoggerTracker : ILoggerTracker
	{
		private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger ( ) ;

		private readonly ConcurrentDictionary < string , ILogger > loggers =
			new ConcurrentDictionary < string , ILogger > ( ) ;

		public ILogger Logger => _Logger ;

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

		public event LoggerRegisteredEventHandler LoggerRegistered ;

		protected virtual void OnLoggerRegistered ( LoggerEventArgs args )
		{
			var handler = LoggerRegistered ;
			handler?.Invoke ( this , args ) ;
		}
	}
}