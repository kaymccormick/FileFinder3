#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// ContextStack.cs
// 
// 2020-01-20-6:15 AM
// 
// ---

#endregion

using System ;
using System.Collections.Generic ;
using System.Collections.Specialized ;
using System.Linq ;
using NLog ;

namespace Common.Context
{
    public class ContextStack<T> : Stack<T> where T : InfoContext
    {
        // not sure this makes sense to set - thread safe and/or conflicts?
        public static bool DefaultAllowDuplicateNames { get; set; } = true;
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public bool AllowDuplicateNames { get; } = DefaultAllowDuplicateNames;

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that is empty and has the default initial capacity.</summary>
        public ContextStack()
        {
        }

         public new void Push(
            T item
        )
        {
            Logger.Trace($"{nameof(ContextStack<InfoContext>)}.Push ( {item}" );
            if (!AllowDuplicateNames && this.Any(context => context.Name == item.Name))
            {
                throw new 
	                DuplicateKeyException(key: item.Name);
            }

            base.Push( item );
            Logger.Trace($"[{(this)}: New count is {Count}"  );
        }
        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that is empty and has the default initial capacity.</summary>
        public ContextStack(
            bool allowDuplicateNames
        )
        {
            AllowDuplicateNames = allowDuplicateNames;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that is empty and has the specified initial capacity or the default initial capacity, whichever is greater.</summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Stack`1" /> can contain.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="capacity" /> is less than zero.</exception>
        public ContextStack(
            int capacity
        ) : base( capacity )
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Stack`1" /> class that contains elements copied from the specified collection and has sufficient capacity to accommodate the number of elements copied.</summary>
        /// <param name="collection">The collection to copy elements from.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="collection" /> is <see langword="null" />.</exception>
        public ContextStack(
            IEnumerable < T > collection
        ) : base( collection )
        {
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{String.Join("/", this.Reverse())}";
        }

        public OrderedDictionary ToOrderedDictionary()
        {
            Stack < T > copyStack = new Stack < T >( this );
            while ( copyStack.Any() )
            {
                var x = copyStack.Pop();
                if ( copyStack.Any( context => context.Name == x.Name ) )
                {
                    throw new DuplicateKeyException( key: x.Name );
                }
            }
            var r = new OrderedDictionary();
            foreach ( var info in this.Reverse() )
            {
                var key = info.Name;
                 r[key] = info.ObjectContext;
            }

            return r;
        }

    }
}