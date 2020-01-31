using System ;
using System.ComponentModel.Composition ;

namespace WpfApp1.Attributes
{
	[ MetadataAttribute ]
	internal class WindowMetadataAttribute : Attribute
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public WindowMetadataAttribute ( string windowTitle ) { WindowTitle = windowTitle ; }

		public string WindowTitle { get ; }
	}
}