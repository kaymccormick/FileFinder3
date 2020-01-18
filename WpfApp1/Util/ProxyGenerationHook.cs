#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// ProxyGenerationHook.cs
// 
// 2020-01-18-3:40 PM
// 
// ---

#endregion

using System;
using System.Reflection;
using Castle.DynamicProxy;
using NLog;

namespace WpfApp1
{
    public class ProxyGenerationHook : IProxyGenerationHook
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public void NonProxyableMemberNotification(
            Type       type,
            MemberInfo memberInfo
        )
        {
        }

        public bool ShouldInterceptMethod(
            Type       type,
            MethodInfo memberInfo
        )
        {
            return memberInfo.Name.StartsWith(
                                              "get_", StringComparison.Ordinal
                                             );
        }

        public void MethodsInspected()
        {
        }

        public void NonVirtualMemberNotification(
            Type       type,
            MemberInfo memberInfo
        )
        {
        }
    }
}