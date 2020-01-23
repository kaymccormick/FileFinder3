#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// InfoContext.cs
// 
// 2020-01-20-7:03 AM
// 
// ---

#endregion

using System ;
using System.Collections ;
using System.Collections.Generic ;

namespace WpfApp1.Util
{
    public class InfoContext : Tuple <string, object>, IEnumerable<object>
    {
        public delegate InfoContext Factory(
            string name,
            object objectContext
        );
        public string Name { get => Item1; }

        public object ObjectContext { get => Item2; }

        public InfoContext(
            string name,
            object objectContext
        ) : base(name, objectContext)
        {
            // Name = name;
            // ObjectContext = objectContext;
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator < object > GetEnumerator()
        {
            yield return Name;
            yield return ObjectContext;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var methodInfo = ObjectContext.GetType().GetMethod("ToString", Type.EmptyTypes); //, BindingFlags.Public | BindingFlags.Instance);
            string s;
            s = methodInfo.DeclaringType == typeof(Object)
                    ? ObjectContext.GetType().Name
                    : ObjectContext.ToString();

            return Name + "=" + s;
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}