using System;
using System.Collections.Generic;
using System.Diagnostics ;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy ;
using WpfApp1.Attributes ;
using WpfApp1.Interfaces ;

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
				Debug.WriteLine ( $"Invocation target has logger" ) ;
				var logger = haveLogger.Logger ;
				if ( logger != null )
				{
					logger.Debug ( $"invocation of {invocation.Method.Name}" ) ;
				}
			}

			invocation.Proceed();

		}
	}
}
