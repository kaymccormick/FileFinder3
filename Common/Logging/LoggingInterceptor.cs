using System ;
using AppShared.Interfaces ;
using Castle.DynamicProxy ;
using Common.Context ;

namespace Common.Logging
{
	public class LoggingInterceptor : IInterceptor
	{
		public void Intercept ( IInvocation invocation )
		{
			var customAttributes = Attribute.GetCustomAttributes (
			                                                      invocation.GetConcreteMethodInvocationTarget ( )
			                                                    , typeof ( PushContextAttribute )
			                                                     ) ;


			if ( invocation.InvocationTarget is IHaveLogger haveLogger )
			{
				var logger = haveLogger.Logger ;
				if ( logger != null )
				{
					logger.Trace ( $"invocation of {invocation.Method.Name}" ) ;
				}
			}

			invocation.Proceed();

		}
	}
}
