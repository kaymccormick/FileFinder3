using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.IO ;
using System.Linq ;
using System.Reflection ;
using System.Runtime.CompilerServices ;
using System.Text ;
using System.Text.RegularExpressions ;
using Castle.DynamicProxy ;
using DynamicData ;
using NLog ;
using NLog.Common ;
using NLog.Config ;
using NLog.Layouts ;
using NLog.Targets ;

namespace Common.Logging
{
	public class AppLoggingConfigHelper : LoggingConfiguration
	{
		public static  StringWriter _stringWriter ;
		private static JsonLayout   _fLayout ;

		[ ThreadStatic ]
		private static Nullable<int> NumTimesConfigured = null ;


		/// <summary>
		///     Gets the collection of file names which should be watched for changes by
		///     NLog.
		/// </summary>
		public override IEnumerable < string > FileNamesToWatch { get ; }

		public MyLogFactory logFactory { get ; set ; }

		public static bool DebuggerTargetEnabled { get ; set ; } = false ;

		public static bool LoggingIsConfigured { get ; set ; }

		public static bool DumpExistingConfig { get ; set ; } = true ;


		public static bool ForceCodeConfig { get ; set ; } = false ;

		/// <summary>
		///     Called by LogManager when one of the log configuration files changes.
		/// </summary>
		/// <returns>
		///     A new instance of <see cref="T:NLog.Config.LoggingConfiguration" /> that
		///     represents the updated configuration.
		/// </returns>
		public override LoggingConfiguration Reload ( ) { return base.Reload ( ) ; }

		private static void DoLogMessage (
			string message
		  , string callerFilePath
		  , string callerMemberName
		)
		{
			System.Diagnostics.Debug.WriteLine (
			                                    callerMemberName
			                                    + ":"
			                                    + nameof ( AppLoggingConfigHelper )
			                                    + ":"
			                                    + message
			                                   ) ;
			// System.Diagnostics.Debug.WriteLine ( nameof(AppLoggingConfigHelper) + ":" + message ) ;
		}

		// ReSharper disable once MemberCanBePrivate.Global
		internal static LoggingConfiguration ConfigureLogging (
			LoggerProxyHelper.LogMethod logMethod
		)
		{
			logMethod ( "*** Starting logger configuration." ) ;
			InternalLogging ( ) ;

			var proxyGenerator = new ProxyGenerator ( ) ;
			var loggerProxyHelper = new LoggerProxyHelper ( proxyGenerator , DoLogMessage ) ;
			var logFactory = new MyLogFactory ( DoLogMessage ) ;
			var lconfLogFactory = loggerProxyHelper.CreateLogFactory ( logFactory ) ;

			var fieldInfo = typeof ( LogManager ).GetField (
			                                                "factory"
			                                              , BindingFlags.Static
			                                                | BindingFlags.NonPublic
			                                               ) ;
			logMethod ( $"field info is {fieldInfo.DeclaringType} . {fieldInfo.Name}" ) ;
			var cur = fieldInfo.GetValue ( null ) ;
			logMethod ( $"cur is {cur}" ) ;

			fieldInfo.SetValue ( null , lconfLogFactory ) ;
			var newVal = fieldInfo.GetValue ( null ) ;
			logMethod ( $"newval is {newVal}" ) ;

			var lconf = new CodeConfiguration ( lconfLogFactory ) ;
			var t = new List < Target > ( ) ;

			#region Cache Target
			var cacheTarget = new MyCacheTarget ( ) ;
			t.Add ( cacheTarget ) ;
			#endregion
			#region NLogViewer Target
			var viewer = Viewer ( ) ;
			t.Add ( viewer ) ;
			#endregion
			#region Debugger Target
			if ( DebuggerTargetEnabled )
			{
				var debuggerTarget =
					new DebuggerTarget { Layout = new SimpleLayout ( "${message}" ) } ;
				t.Add ( debuggerTarget ) ;
			}
			#endregion
			#region Chainsaw Target
			var chainsawTarget = new ChainsawTarget ( ) ;
			SetupNetworkTarget ( chainsawTarget , "udp://192.168.10.1:4445" ) ;
			t.Add ( chainsawTarget ) ;
			#endregion
			t.Add ( MyFileTarget ( ) ) ;
			var jsonFileTarget = JsonFileTarget ( ) ;
			t.Add ( jsonFileTarget ) ;
			var byType = new Dictionary < Type , int > ( ) ;
			var byName = new Dictionary < string , Target > ( ) ;
			foreach ( var target in t )
			{
				// logMethod ( $"target is {target}" ) ;
				var count = 0 ;
				var type = target.GetType ( ) ;
				byType.TryGetValue ( type , out count ) ;
				count          += 1 ;
				byType[ type ] =  count ;

				if ( target.Name == null )
				{
					target.Name = $"{Regex.Replace ( type.Name , "Target" , "" )}{count:D2}" ;
				}


				lconf.AddTarget ( target ) ;
			}

			var loggingRules = t.AsQueryable ( ).Select ( DefaultLoggingRule ) ;
			foreach ( var loggingRule in loggingRules ) { lconf.LoggingRules.Add ( loggingRule ) ; }

			LogManager.Configuration = lconf ;

			var logger = lconfLogFactory.GetLogger ( "test" ) ;
			logger.Debug ( "test123" ) ;
			logMethod ( logger.ToString ( ) ) ;
			return lconf ;
		}

		private static LoggingRule DefaultLoggingRule ( Target target )
		{
			return new LoggingRule ( "*" , LogLevel.FromOrdinal ( 0 ) , target ) ;
		}

		private static void InternalLogging ( )
		{
			InternalLogger.LogLevel = LogLevel.Debug ;

			var id = Process.GetCurrentProcess ( ).Id ;
			var LogFile = $@"c:\temp\nlog-internal-{id}.txt" ;
			InternalLogger.LogFile = LogFile ;
			//"c:\\temp\\nlog-internal-${processid}.txt" ;
			//InternalLogger.LogToConsole      = true ;
			//InternalLogger.LogToConsoleError = true ;
			//InternalLogger.LogToTrace        = true ;

			_stringWriter            = new StringWriter ( ) ;
			InternalLogger.LogWriter = _stringWriter ;
		}

		private static void SetupNetworkTarget ( NetworkTarget target , string address )
		{
			target.Address = new SimpleLayout ( address ) ;
		}

		private static NLogViewerTarget Viewer ( string name = null )
		{
			return new NLogViewerTarget ( name )
			       {
				       Address              = new SimpleLayout ( "udp://10.25.0.102:9999" )
				     , IncludeAllProperties = true
				     , IncludeCallSite      = true
				     , IncludeSourceInfo    = true
			       } ;
		}

		public static FileTarget JsonFileTarget ( )
		{
			var f = new FileTarget ( "OUT.JSON" ) ;
			f.Name     = "json_out" ;
			f.FileName = Layout.FromString ( @"c:\data\logs\${appdomain}-${processid}-out.json" ) ;

			_fLayout = new JsonLayout { IncludeAllProperties = true } ;

			f.Layout = _fLayout ;

			var j = _fLayout ;
			var layout = Layout.FromString ( "${appdomain} ${message}" ) ;
			var messageAttr = new JsonAttribute ( "message" , layout ) ;
			j.Attributes.AddRange ( new[] { messageAttr } ) ;

			return f ;
		}

		public static FileTarget MyFileTarget ( )
		{
			var f = new FileTarget ( ) ;
			f.Name     = "text_log" ;
			f.FileName = Layout.FromString ( @"c:\data\logs\log.txt" ) ;

			f.Layout = Layout.FromString ( "${message}" ) ;
			return f ;
		}

		public static void EnsureLoggingConfigured ( )
		{
			EnsureLoggingConfigured ( DumpExistingConfig , null ) ;
		}

		public static void EnsureLoggingConfigured (
			bool                        b
		  , LoggerProxyHelper.LogMethod logMethod
		  , [ CallerFilePath ] string   callerFilePath = null
		)
		{
			if ( ! NumTimesConfigured.HasValue )
			{
				NumTimesConfigured = 1 ;
			}
			else
			{
				NumTimesConfigured += 1 ;
			}

			if ( logMethod == null )
			{
				logMethod = DoLogMessage ;
			}

			logMethod ( $"[time {NumTimesConfigured.Value}]\t{nameof ( EnsureLoggingConfigured )} called from {callerFilePath}" ) ;


			var fieldInfo2 = LogManager.LogFactory.GetType ( )
			                           .GetField (
			                                      "_config"
			                                    , BindingFlags.Instance | BindingFlags.NonPublic
			                                     ) ;

			object config ;
			if ( fieldInfo2 == null )
			{
				System.Diagnostics.Debug.WriteLine (
				                                    "no field _configLoaded for "
				                                    + LogManager.LogFactory
				                                   ) ;
				throw new Exception ( "no config loaded field found" ) ;
			}

			config = fieldInfo2.GetValue ( LogManager.LogFactory ) ;

			//LogManager.ThrowConfigExceptions = true;
			//LogManager.ThrowExceptions = true;
			var fieldInfo = LogManager.LogFactory.GetType ( )
			                          .GetField (
			                                     "_configLoaded"
			                                   , BindingFlags.Instance | BindingFlags.NonPublic
			                                    ) ;

			bool _configLoaded ;
			if ( fieldInfo == null )
			{
				if ( config != null )
				{
					_configLoaded = true ;
				}
				else
				{
					_configLoaded = false ;
				}

				System.Diagnostics.Debug.WriteLine (
				                                    "no field _configLoaded for "
				                                    + LogManager.LogFactory
				                                   ) ;
				// throw new Exception ( "no config loaded field found" ) ;
			}
			else
			{
				_configLoaded = ( bool ) fieldInfo.GetValue ( LogManager.LogFactory ) ;
			}

			var isMyConfig = ! _configLoaded     || LogManager.Configuration is CodeConfiguration ;
			var doConfig = ! LoggingIsConfigured || ForceCodeConfig && ! isMyConfig ;
			logMethod (
			           $"{nameof ( LoggingIsConfigured )} = {LoggingIsConfigured}; {nameof ( ForceCodeConfig )} = {ForceCodeConfig}; {nameof ( isMyConfig )} = {isMyConfig});"
			          ) ;
			if ( DumpExistingConfig )
			{
				Action < string > collect = s_ => {
					System.Diagnostics.Debug.WriteLine ( s_ ) ;
				} ;
				DoDumpConfig ( collect ) ;
			}

			if ( doConfig )
			{
				ConfigureLogging ( logMethod ) ;
				return ;
			}

			// if ( LogManager.Configuration == null )
			// {
			// 	ConfigureLogging (logMethod ) ;
			// }

			DumpPossibleConfig ( LogManager.Configuration ) ;
		}

		private static void DoDumpConfig ( Action < string > collect )
		{
			var config = LogManager.Configuration ;
			if ( config == null )
			{
				return ;
			}

			foreach ( var target in config.AllTargets )
			{
				foreach ( var atarget in config.AllTargets )
				{
					collect ( atarget.Name ) ;
					collect ( atarget.GetType ( ).ToString ( ) ) ;
					if ( atarget is TargetWithLayout a )
					{
						if ( a.Layout is JsonLayout jl )
						{
							string Selector ( JsonAttribute attribute , int i )
							{
								var b = new StringBuilder ( ) ;
								var propertyInfos = attribute
								                   .GetType ( )
								                   .GetProperties (
								                                   BindingFlags.Public
								                                   | BindingFlags.Instance
								                                  ) ;
								foreach ( var propertyInfo in propertyInfos )
								{
									var val2 = propertyInfo.GetValue ( attribute ) ;
									b.Append ( $"{propertyInfo.Name} = {val2}; " ) ;
								}

								return b.ToString ( ) ;
							}

							var enumerable = jl.Attributes.Select ( Selector ) ;
							collect ( string.Join ( "--" , enumerable ) ) ;
						}
					}

					if ( atarget is FileTarget gt )
					{
						collect ( gt.FileName.ToString ( ) ) ;
					}



					// if(json.Target.Layout is JsonLayout l)
					// {
					// collectaTargeT);
					// }
				}
			}
		}


		private static void DumpPossibleConfig ( LoggingConfiguration configuration )
		{
			var candidateConfigFilePaths = LogManager.LogFactory.GetCandidateConfigFilePaths ( ) ;
			foreach ( var q in candidateConfigFilePaths )
			{
				Debug ( $"{q}" ) ;
			}

			var fieldInfo = configuration.GetType ( )
			                             .GetField (
			                                        "_originalFileName"
			                                      , BindingFlags.NonPublic | BindingFlags.Instance
			                                       ) ;
			if ( fieldInfo != null )
			{
				if ( fieldInfo.GetValue ( configuration ) != null )
				{
					{
						Debug ( "Original NLOG configuration filename" ) ;
					}
				}
			}

			Debug ( $"{configuration}" ) ;
		}


		private static void Debug ( string s ) { }
		// public static  Func < bool > EnsureLoggingConfigured = () => true ;
	}
}