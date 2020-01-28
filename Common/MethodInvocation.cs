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
using System.Reflection ;

namespace Common
{
	public class MethodInvocation
	{
		public MethodInfo Method { get ; set ; }

		public object[] Arguments { get ; set ; }

		public object OriginalReturnValue { get ; set ; }

		public object ReturnValue { get ; set ; }
	}
}