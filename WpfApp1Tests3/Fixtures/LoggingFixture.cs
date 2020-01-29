
using System.Diagnostics;
using Common.Logging ;

namespace WpfApp1Tests3.Fixtures
{
	public class LoggingFixture
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public LoggingFixture ( ) {
			AppLoggingConfigHelper.EnsureLoggingConfigured();
			Debug.WriteLine("MY LogFactory is o " + NLog.LogManager.LogFactory.ToString());
		}
	}
}