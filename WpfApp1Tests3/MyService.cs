#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// MyService.cs
// 
// 2020-01-24-3:30 PM
// 
// ---
#endregion
using WpfApp1.Interfaces ;
using WpfApp1.Util ;

namespace WpfApp1Tests3
{
	public class MyService : IHaveObjectId
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public MyService ( ) {
		}

		public object InstanceObjectId { get ; set ; }

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString ( )
		{
			return $"{nameof(MyService)}: {nameof ( InstanceObjectId )}: {InstanceObjectId}" ;
		}
	}
}