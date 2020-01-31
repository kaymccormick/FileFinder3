using System.Diagnostics ;
using System.Linq ;
using Common ;
using Common.Logging ;
using NLog ;
using NLog.Config ;
using NLog.Targets ;
using Xunit ;
using Xunit.Abstractions ;

namespace CommonTests
{
	public class LogConfigTest
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		/// <param name="output"></param>
		public LogConfigTest ( ITestOutputHelper output )
		{
			_output    = output ;
			_logMethod = LogMethod ;
		}

		private readonly ITestOutputHelper           _output ;
		private readonly LoggerProxyHelper.LogMethod _logMethod ;

		private void LogMethod ( string message , string callerfilepath , string callermembername )
		{
			_output.WriteLine ( message ) ;
			Debug.WriteLine ( message ) ;
		}

		private void CheckLogConfig ( LoggingConfiguration configuration )
		{
			Assert.NotEmpty ( configuration.AllTargets ) ;
			Assert.NotEmpty (
			                 configuration.AllTargets.Select ( target => target is DebugTarget )
			                ) ;
			Assert.NotEmpty (
			                 configuration.AllTargets.Select (
			                                                  target => target is NLogViewerTarget
			                                                 )
			                ) ;
			Assert.NotEmpty (
			                 configuration.AllTargets.Select ( target => target is ChainsawTarget )
			                ) ;
			Assert.NotEmpty (
			                 configuration.AllTargets.Select ( target => target is MyCacheTarget )
			                ) ;

			// var q =
			// from target in configuration.AllTargets
			// join rule in configuration.LoggingRules on target ;
		}

		[ Fact ]
		public void TestEnsureConfigTwoArgs ( )
		{
			AppLoggingConfigHelper.EnsureLoggingConfigured ( true , _logMethod ) ;
			CheckLogConfig ( LogManager.Configuration ) ;
		}
	}
}