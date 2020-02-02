﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// AppInitializeAttribute.cs
// 
// 2020-01-26-7:46 AM
// 
// ---
#endregion
using System.Diagnostics ;
using System.Reflection ;
using System.Windows ;
using Common.Logging ;
using NLog ;
using WpfApp1.Application ;
using Xunit.Sdk ;

namespace WpfApp1Tests3.Attributes
{
    public class AppInitializeAttribute : BeforeAfterTestAttribute
    {
        private static readonly Logger Logger = null ;

        public App MyApp { get ; set ; }
        // LogManager.GetCurrentClassLogger();


        /// <summary>
        ///     This method is called before the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public override void Before ( MethodInfo methodUnderTest )
        {
            LoggerProxyHelper.LogMethod logMethod = LogMethod ;
            
            // Application.LoadComponent (
            // Application.Current
            // , new Uri ( "Applications/App.xaml" )

            // ) ;
            MyApp = Application.Current as App ;


            // TODO - this is horribly broken
            if ( MyApp        != null
                 && MyApp.TCS != null )
            {
                if ( MyApp.TCS.Task != null )
                {
                    logMethod ( "Waiting for task to complete" ) ;
                    MyApp.TCS.Task.Wait ( ) ;
                }
                else
                {
                    logMethod ( "null" ) ;
                }
            }
            else
            {
                logMethod ( "null" ) ;
            }

            base.Before ( methodUnderTest ) ;
        }

        // ReSharper disable once InternalOrPrivateMemberNotDocumented
        // ReSharper disable once MemberCanBeMadeStatic.Local
        // ReSharper disable twice IdentifierTypo
        private void LogMethod ( string message , string callerfilepath , string callermembername )
        {
            Logger?.Debug ( message ) ;
            Debug.WriteLine ( message ) ;
        }
    }
}