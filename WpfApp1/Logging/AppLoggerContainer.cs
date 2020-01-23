using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Config;

namespace WpfApp1.Logging
{
	public class AppLoggerContainer
	{
		public AppLogger AppLogger { get; set; }

		public string InternalLog
		{
			get
			{
				if ( Logging.AppLoggingConfigHelper._stringWriter != null )
				{
					return Logging.AppLoggingConfigHelper._stringWriter.ToString();
				}
				else
				{
					return "";
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public AppLoggerContainer()
		{
			Logging.AppLoggingConfigHelper.EnsureLoggingConfigured();
		}

		public LoggingConfiguration Configuration
		{
			get
			{
				return LogManager.Configuration;
			}
		}
		public string Config
		{
			get
			{
				return String.Join( "; ", LogManager.Configuration.ConfiguredNamedTargets.Select( (target,
				                                                                                   i
				                                                                                  ) => target.Name ) );
			}
		}
	}
}
