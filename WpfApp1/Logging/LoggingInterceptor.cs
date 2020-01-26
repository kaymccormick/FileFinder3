using System;
using System.Collections.Generic;
using System.Diagnostics ;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppShared.Interfaces ;
using Castle.DynamicProxy ;
using WpfApp1.Attributes ;

namespace WpfApp1.Logging
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
