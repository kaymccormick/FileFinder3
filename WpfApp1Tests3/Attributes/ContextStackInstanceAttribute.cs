using System;

namespace WpfApp1Tests3.Attributes
{
    [ AttributeUsage( AttributeTargets.Property ) ]
    public class ContextStackInstanceAttribute
        : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Attribute" /> class.</summary>
        public ContextStackInstanceAttribute()
        {
        }
    }
}