#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// XunitTarget.cs
// 
// 2020-01-22-9:21 AM
// 
// ---
#endregion

using NLog ;
using NLog.Targets ;
using Xunit.Abstractions ;

namespace WpfApp1Tests3
{
	[ Target ( "Xunit" ) ]
	public class XunitTarget : TargetWithLayout
	{
		private readonly ITestOutputHelper _outputHelper ;

		public XunitTarget ( ITestOutputHelper outputHelper ) { _outputHelper = outputHelper ; }

		/// <summary>
		///     Writes logging event to the log target. Must be overridden in inheriting
		///     classes.
		/// </summary>
		/// <param name="logEvent">Logging event to be written out.</param>
		public new void Write ( LogEventInfo logEvent )
		{
			var renderLogEvent = RenderLogEvent ( Layout , logEvent ) ;
			_outputHelper.WriteLine ( renderLogEvent ) ;
		}
	}
}