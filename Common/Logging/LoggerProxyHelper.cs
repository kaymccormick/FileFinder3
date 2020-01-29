using System.Reflection ;
using Castle.DynamicProxy ;
using NLog ;
using NLog.Fluent ;

namespace Common.Logging
{
	public class LoggerProxyHelper
	{
		public ProxyGenerator Generator { get ; }

		public delegate void LogMethod ( string message ) ;

		public LogMethod UseLogMethod { get ; set ; }
		/// <summary>Initializes a new instance of the <see
		/// cref="T:System.Object" /> class.</summary>
		public LoggerProxyHelper ( ProxyGenerator generator ) { Generator = generator ; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public LoggerProxyHelper ( ProxyGenerator generator , LogMethod logMethod )
		{
			Generator = generator ;
			UseLogMethod = logMethod ;
		}

		public LogFactory CreateLogFactory ( LogFactory logFactory )
		{
			if ( logFactory == null )
			{
				logFactory = LogManager.LogFactory ;
			}

			return Generator.CreateClassProxyWithTarget (
			                                             LogManager.LogFactory
			                                           , new LogFactoryInterceptor ( Generator , UseLogMethod
			                                            ) );
		}
	}
	
}
