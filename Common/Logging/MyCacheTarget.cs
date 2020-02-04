using System ;
using System.Linq ;
using System.Reactive.Linq ;
using System.Reactive.Subjects ;
using Logging ;
using NLog ;
using NLog.Config ;
using NLog.Targets ;

namespace Common.Logging
{
	[ Target ( nameof ( MyCacheTarget ) ) ]
	public class MyCacheTarget : Target
	{
		// ##############################################################################################################################
		// Constructor
		// ##############################################################################################################################

		#region Constructor
		public MyCacheTarget ( )
		{
			_CacheSubject = new ReplaySubject < LogEventInfo > ( MaxCount ) ;
		}
		#endregion

		/// <summary>
		///     If there is no target in nlog.config definedm a new one is registered with
		///     the default maxcount
		/// </summary>
		/// <param name="defaultMaxCount"></param>
		/// <returns></returns>
		public static MyCacheTarget GetInstance ( int defaultMaxCount )
		{
			AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ;
			var target =
				( MyCacheTarget ) LogManager.Configuration.AllTargets.FirstOrDefault (
				                                                                      t
					                                                                      => t is
						                                                                         MyCacheTarget
				                                                                     ) ;
			if ( target == null )
			{
				target = new MyCacheTarget
				         {
					         MaxCount = defaultMaxCount , Name = nameof ( MyCacheTarget )
				         } ;
				LogManager.Configuration.AddTarget ( target.Name , target ) ;
				LogManager.Configuration.LoggingRules.Insert (
				                                              0
				                                            , new LoggingRule (
				                                                               "*"
				                                                             , LogLevel.FromString (
				                                                                                    "Trace"
				                                                                                   )
				                                                             , target
				                                                              )
				                                             ) ;
				LogManager.ReconfigExistingLoggers ( ) ;
			}

			return target ;
		}

		// ##############################################################################################################################
		// override
		// ##############################################################################################################################

		#region override
		protected override void Write ( LogEventInfo logEvent )
		{
			_CacheSubject.OnNext ( logEvent ) ;
		}
		#endregion

		// ##############################################################################################################################
		// Properties
		// ##############################################################################################################################

		#region Properties
		// ##########################################################################################
		// Public Properties
		// ##########################################################################################

		/// <summary>
		///     The maximum amount of entries held
		/// </summary>
		[ RequiredParameter ]
		public int MaxCount { get ; set ; } = 1000 ;

		public IObservable < LogEventInfo > Cache => _CacheSubject.AsObservable ( ) ;

		private readonly ReplaySubject < LogEventInfo > _CacheSubject ;

		// ##########################################################################################
		// Private Properties
		// ##########################################################################################
		#endregion
	}
}