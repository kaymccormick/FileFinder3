#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// AttachedContext.cs
// 
// 2020-01-22-9:21 AM
// 
// ---

#endregion

using System;
using System.Linq;
using WpfApp1Tests3.Utils;
using Xunit;

namespace WpfApp1Tests3
{
	public class AttachedContext : IDisposable
	{
		private readonly InfoContext                  _infoContext;
		private readonly ContextStack < InfoContext > contextStack;

		public AttachedContext(
			ContextStack < InfoContext > contextStack,
			InfoContext                  context
		)
		{
			this.contextStack = contextStack;
			_infoContext      = context;
			contextStack.Push( _infoContext );
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing,
		///     or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Assert.NotEmpty( contextStack );
			Assert.True( ReferenceEquals( _infoContext, contextStack.First() ) );
			contextStack.Pop();
		}
	}
}