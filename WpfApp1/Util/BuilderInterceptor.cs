#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// BuilderInterceptor.cs
// 
// 2020-01-26-3:30 AM
// 
// ---
#endregion
using System ;
using System.Collections.Generic ;
using System.Linq.Expressions ;
using System.Reflection ;
using System.Windows.Documents ;
using Castle.DynamicProxy ;
using NLog ;

namespace WpfApp1.Util
{
	public class BuilderInterceptor : IInterceptor
	{
		private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger ( ) ;
		public ProxyGenerator ProxyGenerator { get ; }

		public BuilderInterceptor ( ProxyGenerator proxyGenerator )
		{
			ProxyGenerator = proxyGenerator ;
		}

		public List < MethodInvocation > Invocations { get ; } = new List < MethodInvocation > ( ) ;

		public void Intercept ( IInvocation invocation )
		{
			var i = new MethodInvocation ( ) ;
			;
			i.Method    = invocation.Method ;
			i.Arguments = invocation.Arguments ;
			Invocations.Add ( i ) ;
			invocation.Proceed ( ) ;
			i.ReturnValue = invocation.ReturnValue ;
			try
			{

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
	}

	public class MethodInvocation
	{
		public MethodInfo Method { get ; set ; }

		public object[] Arguments { get ; set ; }

		public object ReturnValue { get ; set ; }
	}
}