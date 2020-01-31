#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// ProvideValueTarget.cs
// 
// 2020-01-22-9:21 AM
// 
// ---
#endregion

using System.Windows.Markup ;

namespace WpfApp1Tests3
{
	internal class ProvideValueTarget : IProvideValueTarget
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public ProvideValueTarget ( object targetObject , object targetProperty )
		{
			TargetObject   = targetObject ;
			TargetProperty = targetProperty ;
		}

		/// <summary>Gets the target object being reported.</summary>
		/// <returns>The target object being reported.</returns>
		public object TargetObject { get ; }

		/// <summary>Gets an identifier for the target property being reported.</summary>
		/// <returns>An identifier for the target property being reported.</returns>
		public object TargetProperty { get ; }
	}
}