using System.Collections.Generic ;
using NLog.Config ;

namespace Common.Logging
{
	public class CodeConfiguration : LoggingConfiguration
	{
		/// <summary>
		/// Called by LogManager when one of the log configuration files changes.
		/// </summary>
		/// <returns>
		/// A new instance of <see cref="T:NLog.Config.LoggingConfiguration" /> that represents the updated configuration.
		/// </returns>
		public override LoggingConfiguration Reload ( ) { return base.Reload ( ) ; }

		/// <summary>
		/// Gets the collection of file names which should be watched for changes by NLog.
		/// </summary>
		public override IEnumerable < string > FileNamesToWatch { get ; }
	}
}
