using System.Reflection ;
using Castle.DynamicProxy ;
using NLog ;
using NLog.Fluent ;

namespace Common.Logging
{
	public class LoggerProxyHelper
	{
		public ProxyGenerator Generator { get ; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public LoggerProxyHelper ( ProxyGenerator generator ) { Generator = generator ; }

		public LogFactory CreateLogFactory ( LogFactory logFactory )
		{
			if ( logFactory == null )
			{
				logFactory = LogManager.LogFactory ;
			}

			return Generator.CreateClassProxyWithTarget (
			                                             LogManager.LogFactory
			                                           , new LogFactoryInterceptor ( Generator )
			                                            ) ;
		}
	}
	
}
