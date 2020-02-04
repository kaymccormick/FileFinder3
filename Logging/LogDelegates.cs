using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices ;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class LogDelegates
    {
        // public delegate void LogMethod (
            // string                      message
          // , [ CallerFilePath ]   string callerFilePath   = ""
          // , [ CallerMemberName ] string callerMemberName = ""
        // ) ;
        public delegate void LogMethod (
            string                      message
          , [ CallerFilePath ]   string callerFilePath   = ""
          , [ CallerMemberName ] string callerMemberName = ""
        ) ;
    }
}
