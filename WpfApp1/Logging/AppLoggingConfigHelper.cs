using System ;
using System.Collections.Generic ;
using System.IO ;
using System.Linq ;
using System.Linq.Dynamic ;
using System.Reflection ;
using System.Text.RegularExpressions ;
using Microsoft.Scripting.Utils ;
using NLog ;
using NLog.Common ;
using NLog.Config ;
using NLog.Layouts ;
using NLog.Targets ;

namespace WpfApp1.Logging
{
	internal static class AppLoggingConfigHelper
	{
		public static StringWriter _stringWriter = null ;

		public static LoggingConfiguration ConfigureLogging (   )
		{
			InternalLogging (   ) ;

			var lconf = new LoggingConfiguration (   ) ;

			var t = new List < Target > (   ) ;

			#region Cache Target
			var cacheTarget = new MyCacheTarget (   ) ;
			t.Add ( cacheTarget ) ;
			#endregion
			#region NLogViewer Target
			var viewer = Viewer ( ) ;
			t.Add ( viewer ) ;
			#endregion
			#region Debugger Target
			var debuggerTarget = new DebuggerTarget
			                     {
				                     Layout = new SimpleLayout ( "${message}" )
			                     } ;
			t.Add ( debuggerTarget ) ;
			#endregion
			#region Chainsaw Target
			var chainsawTarget = new ChainsawTarget (   ) ;
			SetupNetworkTarget ( chainsawTarget , "udp://192.168.10.1:4445" ) ;
			t.Add ( chainsawTarget ) ;
			#endregion
			Dictionary < Type , int > byType = new Dictionary < Type , int > ( ) ;
			Dictionary < string , Target > byName = new Dictionary < string , Target > ( ) ;
			foreach ( var target in t )
			{
				int count  =  0 ;
				var type = target.GetType (     ) ;
				byType.TryGetValue ( type , out count ) ;
				count                           += 1 ;
				byType[ type ] =  count ;

				if ( target.Name == null )
				{
					target.Name = $"{Regex.Replace(type.Name, "Target", "")}{count:D2}" ;
				}


				lconf.AddTarget ( target ) ;
			}

			lconf.LoggingRules.AddRange ( t.AsQueryable().Select ( DefaultLoggingRule ) ) ;
			LogManager.Configuration = lconf ;
			LogManager.GetCurrentClassLogger (  ).Info ( "Logging configured" ) ;
			return lconf ;
		}

		private static LoggingRule DefaultLoggingRule ( Target target )
		{
			return new LoggingRule ( "*" , LogLevel.FromOrdinal ( 0 ) , target ) ;
		}

		private static void InternalLogging (  )
		{
			InternalLogger.LogLevel = LogLevel.Debug ;

			InternalLogger.LogFile           = "c:\\temp\\mylog.txt" ;
			InternalLogger.LogToConsole      = true ;
			InternalLogger.LogToConsoleError = true ;
			InternalLogger.LogToTrace        = true ;

			_stringWriter            = new StringWriter (  ) ;
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

		public static void EnsureLoggingConfigured (  )
		{
			//LogManager.ThrowConfigExceptions = true;
			//LogManager.ThrowExceptions = true;
			if ( LogManager.Configuration == null )
			{
				ConfigureLogging (  ) ;
			}

			DumpPossibleConfig ( LogManager.Configuration ) ;
		}

		private static void DumpPossibleConfig ( LoggingConfiguration configuration )
		{
			var candidateConfigFilePaths = LogManager.LogFactory.GetCandidateConfigFilePaths (  ) ;
			foreach ( var q in candidateConfigFilePaths )
			{
				Debug ( $"{q}" ) ;
			}

			var fieldInfo = configuration.GetType (  )
			                             .GetField (
			                                        "_originalFileName"
			                                      , BindingFlags.NonPublic | BindingFlags.Instance
			                                       ) ;
			if ( fieldInfo != null )
			{
				Debug (
				       $"Original NLOG configuration filename is {fieldInfo.GetValue ( configuration )}"
				      ) ;
			}

			Debug ( $"{configuration}" ) ;
		}

		private static void Debug ( string s ) { }
	}
}