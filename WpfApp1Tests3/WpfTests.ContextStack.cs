#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// WpfTests.ContextStack.cs
// 
// 2020-01-20-4:55 AM
// 
// ---

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp1Tests3
{
    public partial class WpfTests
    {
        public class ContextStack<T> : Stack<T> where T : InfoContext
        {
            /// <summary>Returns a string that represents the current object.</summary>
            /// <returns>A string that represents the current object.</returns>
            public override string ToString()
            {
                return $"{String.Join("/", this.Reverse())}";
            }
        }
    }
}