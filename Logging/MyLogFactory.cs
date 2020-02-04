using System.Diagnostics ;
using NLog ;

namespace Logging
{
    public class MyLogFactory : LogFactory
    {
        private readonly LogDelegates.LogMethod _doLogMessage ;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:NLog.LogFactory" /> class.
        /// </summary>
        public MyLogFactory ( ) { }

        public MyLogFactory ( LogDelegates.LogMethod doLogMessage )
        {
            _doLogMessage = doLogMessage ;
        }

        public virtual LogDelegates.LogMethod GetDoLogMessage ( ) { return _doLogMessage ; }

        public new Logger GetLogger ( string name )
        {
            var logger = base.GetLogger ( name ) ;
            if ( GetDoLogMessage ( ) != null )
            {
                GetDoLogMessage ( ) ( $"{name} = {logger}" ) ;
            }
            else
            {
                Debug.WriteLine ( "oops" ) ;
            }

            return logger ;
        }
    }
}