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

using System ;
using System.Diagnostics.CodeAnalysis ;
using System.Reflection ;
using Castle.DynamicProxy ;
using NLog ;

namespace WpfApp1.Util
{
	public class ProxyGenerationHook : IProxyGenerationHook
	{
		[ SuppressMessage (
			                  "Code Quality"
			                , "IDE0052:Remove unread private members"
			                , Justification = "<Pending>"
		                  ) ]
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		public void NonProxyableMemberNotification ( Type type , MemberInfo memberInfo )
		{
			Logger.Debug (
			              $"{nameof ( NonProxyableMemberNotification )}: type={type}, memberInfo={memberInfo}"
			             ) ;
		}

		public bool ShouldInterceptMethod ( Type type , MethodInfo memberInfo )
		{
			Logger.Debug (
			              $"{nameof ( ShouldInterceptMethod )}: type={type}, memberInfo={memberInfo}"
			             ) ;
			return memberInfo.Name.StartsWith ( "get_" , StringComparison.Ordinal ) ;
		}

		public void MethodsInspected ( ) { Logger.Debug ( $"{nameof ( MethodsInspected )}" ) ; }
	}
}