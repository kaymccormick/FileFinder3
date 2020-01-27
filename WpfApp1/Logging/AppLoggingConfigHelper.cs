using System ;
using System.Collections.Generic ;
using System.Configuration ;
using System.Diagnostics ;
using System.IO ;
using System.Linq ;
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
    internal class AppLoggingConfigHelper : LoggingConfiguration
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

        public static StringWriter _stringWriter = null ;

        // ReSharper disable once MemberCanBePrivate.Global
        internal static LoggingConfiguration ConfigureLogging ( )
        {
            InternalLogging ( ) ;

            var lconf = new LoggingConfiguration ( ) ;

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
            t.Add ( JsonFileTarget ( ) ) ;
            var byType = new Dictionary < Type , int > ( ) ;
            var byName = new Dictionary < string , Target > ( ) ;
            foreach ( var target in t )
            {
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

            lconf.LoggingRules.AddRange ( t.AsQueryable ( ).Select ( DefaultLoggingRule ) ) ;
            LogManager.Configuration = lconf ;
            LogManager.GetCurrentClassLogger ( ).Info ( "Logging configured" ) ;
            return lconf ;
        }

        public static bool DebuggerTargetEnabled { get ; set ; } = false ;

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

            f.Layout   = new JsonLayout ( ) { IncludeAllProperties = true } ;

            var j = new JsonLayout ( ) ;
            var layout = Layout.FromString ( "${appdomain} ${message}" ) ;
            var messageAttr= new JsonAttribute ( "message" , layout  );
            j.Attributes.AddRange ( new JsonAttribute[] { messageAttr } ) ;

            return f ;
        }

        public static void EnsureLoggingConfigured ( object LoggingIsConfigured )
        {

            //LogManager.ThrowConfigExceptions = true;
            //LogManager.ThrowExceptions = true;
            if ( LoggingIsConfigured
                 && ForcdcCodeConig
                 && ( LogManager.Configuration is CodeConfiguration ) )
            {
                if ( DumpExistingConfig )
                {
                    Action<string> collect = (s_) => {
                        Debug.WriteLine ( s_ ) ;
                    } ;
                    DoDumpConfig ( collect ) ;
                }

                ConfigureLogging ( ) ;
                return ;

            }
            if ( LogManager.Configuration == null )
            {
                ConfigureLogging ( ) ;
            }

            DumpPossibleConfig ( LogManager.Configuration ) ;
        }

        public static bool DumpExistingConfig { get ; set ; }

        private static void DoDumpConfig ( Action < string > collect )
        {
            var config = LogManager.Configuration ;
            foreach ( var target in config.AllTargets)
            {
                foreach ( var target in config.AllTargets )
                {
                    collect ( target.Name ) ;
                    colect ( target.GetType ( ) ) ;
                    if ( target is FileTarget gt )
                    {
                        collect ( gt.FileName ) ;
                    }
                


                JsonFileTarget json = target.Layout.Layout is JSonLayout layout = `;
                    if(json != null) {
                        json.Attributes.
}
}


                    
            }
            
        }

        public static object ForcdcCodeConig { get ; set ;  = true;


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
                if ( FieldInfo.GetValue ( configuration ) )
                {
                    Debug ( $"Original NLOG configuration filename" ) ;
                }
            }

            Debug ( $"{configuration}" ) ;
        }

        private static void Debug ( string s ) { }
    }
};