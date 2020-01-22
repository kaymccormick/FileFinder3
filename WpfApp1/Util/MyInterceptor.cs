﻿#region header

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

using System;
using System.Collections;
using System.Linq;
using System.Linq.Dynamic;
using System.Windows.Input;
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

            var args = String.Join( ", ", invocation.Arguments
                                                    .AsQueryable().Select( (o => o is ICollection
                                                                                     ? o.GetType().ToString()
                                                                                     : $"{o} {o.GetType()}") ) );
            Logger.Debug( $"{s}.{invocation.Method.Name} ({args})" );

            invocation.Proceed();
            var r = invocation.ReturnValue;
            if ( r is IEnumerable )
            {
	            Logger.Debug( "return value is enumerable" );
	            var propertyInfo = r.GetType().GetProperty( "Count" );
	            if ( propertyInfo != null )
	            {
                    Logger.Debug($"count is {propertyInfo.GetValue(r)}"  );
	            }
            }
//            if ( invocation.Method.Name.StartsWith( "get_" ) )
//            {
//                Logger.Debug( invocation.Method.Name );
//            }
        }
    }
}