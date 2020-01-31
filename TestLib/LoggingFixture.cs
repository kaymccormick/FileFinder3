using System.Diagnostics ;
using Common.Logging ;
using JetBrains.Annotations ;
using Xunit.Abstractions ;
using Xunit.Sdk ;

namespace TestLib
{
	[ UsedImplicitly ]
	public class LoggingFixture
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public LoggingFixture ( IMessageSink sink )
		{
			Sink = sink ;
			AppLoggingConfigHelper.EnsureLoggingConfigured ( false , LogMethod ) ;
			// Debug.WriteLine("MY LogFactory is o " + NLog.LogManager.LogFactory.ToString());
		}

		public IMessageSink Sink { get ; }

		// ReSharper disable once IdentifierTypo
		private void LogMethod ( string message , string callerfilepath , string callermembername )
		{
			Sink.OnMessage ( new DiagnosticMessage ( message ) ) ;
			Debug.WriteLine ( message ) ;
		}
	}
}