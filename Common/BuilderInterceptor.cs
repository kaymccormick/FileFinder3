using System ;
using System.Collections.Generic ;
using Autofac.Builder ;
using Autofac.Core ;
using Castle.DynamicProxy ;
using NLog ;

namespace Common
{
	public class BuilderInterceptor : IInterceptor
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		public BuilderInterceptor ( ProxyGenerator proxyGenerator )
		{
			ProxyGenerator = proxyGenerator ;
		}

		public ProxyGenerator ProxyGenerator { get ; }

		public List < MethodInvocation > Invocations { get ; } = new List < MethodInvocation > ( ) ;

		public void Intercept ( IInvocation invocation )
		{
			var i = new MethodInvocation
			        {
				        Method = invocation.Method , Arguments = invocation.Arguments
			        } ;
			Invocations.Add ( i ) ;
			invocation.Proceed ( ) ;
			i.OriginalReturnValue = invocation.ReturnValue ;
			try
			{
				if ( i.OriginalReturnValue is DeferredCallback cb )
				{
					var cbAction = cb.Callback ;
					var ret = CreateCallbackProxy ( ProxyGenerator , cbAction , cb ) ;
					invocation.ReturnValue = ret ;
					i.ReturnValue          = ret ;
					return ;
				}

				var classProxyWithTarget =
					ProxyGenerator.CreateClassProxyWithTarget (
					                                           invocation.ReturnValue
					                                         , new BuilderInterceptor (
					                                                                   ProxyGenerator
					                                                                  )
					                                          ) ;
				invocation.ReturnValue = classProxyWithTarget ;
			}
			catch ( Exception ex )
			{
				Logger.Warn ( ex , ex.Message ) ;
			}
		}

		public static object CreateCallbackProxy (
			ProxyGenerator                proxyGenerator
		  , Action < IComponentRegistry > callback
		   ,
			// ReSharper disable once UnusedParameter.Global
			DeferredCallback defer
		)

		{
			var x = proxyGenerator.CreateClassProxy (
			                                         typeof ( DeferredCallback )
			                                       , new object[] { callback }
			                                       , new BuilderInterceptor ( proxyGenerator )
			                                        ) ;
			return x ;
		}
	}
}