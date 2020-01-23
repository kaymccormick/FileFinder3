using Xunit.Abstractions;

namespace WpfApp1Tests3.Fixtures
{
    public class LogFixture
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public LogFixture(ITestOutputHelper helper)
        {
            NLog.LogManager.GetCurrentClassLogger().Debug($"{helper}"  );
            helper.WriteLine( "Test log message");
        }
    }
}