using System.Reflection ;
using Common.Logging ;
using JetBrains.Annotations ;
using Logging ;
using NLog ;
using NLog.Config ;
using NLog.Layouts ;
using NLog.Targets ;
using Xunit.Sdk ;

namespace TestLib.Attributes
{
	public class BeforeAfterLoggerAttribute : BeforeAfterTestAttribute
	{
		private const string Name = "test target" ;

		public LoggingRule TestLoggingRule { get ; set ; }

		public FileTarget TestFileTarget { get ; set ; }

		/// <summary>
		///     This method is called after the test method is executed.
		/// </summary>
		/// <param name="methodUnderTest">The method under test</param>
		public override void After ( MethodInfo methodUnderTest )

		{
			LogManager.LogFactory.Configuration.LoggingRules.Remove ( TestLoggingRule ) ;
			LogManager.LogFactory.Configuration.RemoveTarget ( Name ) ;
			LogManager.LogFactory.ReconfigExistingLoggers ( ) ;
		}

		/// <summary>
		///     This method is called before the test method is executed.
		/// </summary>
		/// <param name="methodUnderTest">The method under test</param>
		public override void Before ( [ NotNull ] MethodInfo methodUnderTest )
		{
			AppLoggingConfigHelper.EnsureLoggingConfigured(true, null);
			TestFileTarget = new FileTarget ( Name ) ;
			var fileTarget = TestFileTarget ;
			fileTarget.FileName = Layout.FromString (
			                                         "test-"
			                                         + methodUnderTest.DeclaringType
			                                         + "."
			                                         + methodUnderTest.Name
			                                         + ".txt"
			                                        ) ;
			LogManager.LogFactory.Configuration.AddTarget ( fileTarget ) ;
			var loggingRule = new LoggingRule ( "*" , LogLevel.Trace , fileTarget ) ;
			TestLoggingRule = loggingRule ;
			LogManager.LogFactory.Configuration.LoggingRules.Insert ( 0 , loggingRule ) ;
			LogManager.LogFactory.ReconfigExistingLoggers ( ) ;
		}
	}
}