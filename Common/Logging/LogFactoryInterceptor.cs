#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// Common
// LogFactoryInterceptor.cs
// 
// 2020-01-28-11:01 PM
// 
// ---
#endregion
using Castle.DynamicProxy ;

namespace Common.Logging
{
	public class LogFactoryInterceptor : IInterceptor
	{
		public ProxyGenerator Generator { get ; }

		public LogFactoryInterceptor ( ProxyGenerator generator ) { Generator = generator ; }

		public void Intercept ( IInvocation invocation )
		{
			if ( invocation.Method.Name == "GetCurrentClassLogger" )
			{
				invocation.Proceed();
				var classProxyWithTarget = Generator.CreateClassProxyWithTarget (
				                                                                 invocation.ReturnValue
				                                                               , new LoggerInterceptor ( )
				                                                                ) ;
			}
		}
	}
}