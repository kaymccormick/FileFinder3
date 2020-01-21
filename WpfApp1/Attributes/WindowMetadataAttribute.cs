using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Attributes
{
    [System.ComponentModel.Composition.MetadataAttribute]
    class WindowMetadataAttribute : Attribute
    {
        public string WindowTitle { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public WindowMetadataAttribute(
            string windowTitle
        )
        {
            WindowTitle = windowTitle;
        }
    }
}
