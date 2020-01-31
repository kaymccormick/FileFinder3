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
		public LogFactoryInterceptor (
			ProxyGenerator              generator
		  , LoggerProxyHelper.LogMethod useLogMethod
		)
		{
			Generator    = generator ;
			UseLogMethod = useLogMethod ;
		}

		public ProxyGenerator Generator { get ; }

		public LoggerProxyHelper.LogMethod UseLogMethod { get ; }

		public void Intercept ( IInvocation invocation )
		{
			UseLogMethod ( $"Method name is {invocation.Method.Name}" ) ;
			if ( invocation.Method.Name == "GetLogger" )
			{
				invocation.Proceed ( ) ;
				var classProxyWithTarget = Generator.CreateClassProxyWithTarget (
				                                                                 invocation
					                                                                .ReturnValue
				                                                               , new
					                                                                 LoggerInterceptor (
					                                                                                    Generator
					                                                                                  , UseLogMethod
					                                                                                   )
				                                                                ) ;
			}
			else
			{
				invocation.Proceed ( ) ;
			}
		}
	}
}