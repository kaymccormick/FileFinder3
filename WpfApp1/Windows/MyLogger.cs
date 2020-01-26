#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// MyLogger.cs
// 
// 2020-01-25-9:37 PM
// 
// ---
#endregion
using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using Sentinel.Interfaces ;

namespace WpfApp1.Windows
{
	public class MyLogger : Sentinel.Interfaces.ILogger
	{
		/// <summary>Occurs when a property value changes.</summary>
		public event PropertyChangedEventHandler PropertyChanged ;

		/// <summary>
		/// Gets the entries for the logger.
		/// </summary>
		public IEnumerable < ILogEntry > Entries { get ; set ; }

		/// <summary>
		/// Gets or sets the name of the logger.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Indicates whether new entries are added to the Entries collection.
		/// </summary>
		public bool Enabled { get ; set ; }

		/// <summary>
		/// Gets the newly added entries for the logger.
		/// </summary>
		public IEnumerable < ILogEntry > NewEntries { get ; set ; }

		/// <summary>
		/// Clear the log entries.
		/// </summary>
		public void Clear ( ) { throw new NotImplementedException ( ) ; }

		/// <summary>
		/// Add a batch of new messages to the logger.
		/// </summary>
		/// <param name="entries">Ordered list/queue of items to add.</param>
		public void AddBatch ( Queue < ILogEntry > entries )
		{
			
		}
	}
}