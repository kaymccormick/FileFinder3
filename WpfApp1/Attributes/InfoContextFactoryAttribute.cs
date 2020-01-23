using System ;

namespace WpfApp1.Attributes
{
    [AttributeUsage( AttributeTargets.Property)]
    public class InfoContextFactoryAttribute
        : Attribute
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Attribute" /> class.</summary>
        public InfoContextFactoryAttribute()
        {
        }
    }
}