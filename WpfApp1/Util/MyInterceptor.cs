#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// MyInterceptor.cs
// 
// 2020-01-18-3:40 PM
// 
// ---

#endregion

using Castle.DynamicProxy;
using NLog;

namespace WpfApp1.Util
{
    public class MyInterceptor : IInterceptor
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public void Intercept(
            IInvocation invocation
        )
        {
            var q = invocation.InvocationTarget.ToString();
            var s = invocation.InvocationTarget.GetType().ToString();
            if ( q != s )
            {
                q += $" [{s}]";
            }
            Logger.Info($"{q}.{invocation.Method.Name}"  );
            invocation.Proceed();
//            if ( invocation.Method.Name.StartsWith( "get_" ) )
//            {
//                Logger.Debug( invocation.Method.Name );
//            }
        }
    }
}
