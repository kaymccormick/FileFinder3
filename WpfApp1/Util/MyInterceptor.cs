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
            Logger.Debug($"{nameof(Intercept)}"  );
            invocation.Proceed();
            if ( invocation.Method.Name.StartsWith( "get_" ) )
            {
                Logger.Debug( invocation.Method.Name );
            }
        }
    }
}