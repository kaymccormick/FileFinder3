using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Fluent;

namespace WpfApp1.Interfaces
{
    public class MyLogBuilder : LogBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyLogBuilder"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="Logger"/> to send the log event.</param>
        public MyLogBuilder(
            NLog.ILogger logger
        ) : base( logger )
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="MyLogBuilder"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="Logger"/> to send the log event.</param>
        /// <param name="logLevel">The <see cref="LogLevel"/> for the log event.</param>
        public MyLogBuilder(
            NLog.ILogger  logger,
            LogLevel logLevel
        ) : base( logger, logLevel )
        {
        }


    }

    interface ILogger
    {
    }
}
