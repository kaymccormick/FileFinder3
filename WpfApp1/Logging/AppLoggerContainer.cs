using System.Linq ;
using Common.Logging ;
using Logging ;
using NLog.Config ;
using LogManager = NLog.LogManager ;

namespace WpfApp1.Logging
{
	public class AppLoggerContainer
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public AppLoggerContainer ( ) { AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ; }

		public AppLogger AppLogger { get ; set ; }

		public string InternalLog
		{
			get
			{
				if ( AppLoggingConfigHelper._stringWriter != null )
				{
					return AppLoggingConfigHelper._stringWriter.ToString ( ) ;
				}

				return "" ;
			}
		}

		public LoggingConfiguration Configuration => LogManager.Configuration ;

		public string Config
		{
			get
			{
				return string.Join (
				                    "; "
				                  , LogManager.Configuration.ConfiguredNamedTargets.Select (
				                                                                            (
					                                                                            target
					                                                                          , i
				                                                                            ) => target
					                                                                           .Name
				                                                                           )
				                   ) ;
			}
		}
	}
}