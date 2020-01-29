﻿
using System.Diagnostics;
using Common.Logging ;
using Xunit.Abstractions ;
using Xunit.Sdk ;

namespace WpfApp1Tests3.Fixtures
{
	public class LoggingFixture
	{
		public IMessageSink Sink { get ; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public LoggingFixture (IMessageSink sink) {
			Sink = sink ;
			AppLoggingConfigHelper.EnsureLoggingConfigured(false, LogMethod);
			Debug.WriteLine("MY LogFactory is o " + NLog.LogManager.LogFactory.ToString());
		}

		private void LogMethod ( string message , string callerfilepath , string callermembername )
		{
			Sink.OnMessage ( new DiagnosticMessage ( message ) ) ;
		}
	}
}