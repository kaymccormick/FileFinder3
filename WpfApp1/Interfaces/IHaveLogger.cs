#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// IHaveLogger.cs
// 
// 2020-01-22-1:30 PM
// 
// ---

#endregion

using NLog;

namespace WpfApp1.Interfaces
{
	public interface IHaveLogger

	{
		ILogger Logger { get; set; }
	}
}