#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// IHaveAppLogger.cs
// 
// 2020-01-25-1:10 PM
// 
// ---
#endregion
using Common.Logging ;

namespace WpfApp1.Application
{
	public interface IHaveAppLogger
	{
		AppLogger AppLogger { get ; set ; }
	}
}