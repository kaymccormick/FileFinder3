#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// LoggingEntityMetadataAttribute.cs
// 
// 2020-01-22-7:29 AM
// 
// ---

#endregion

using System;

namespace WpfApp1.Menus
{
	[System.ComponentModel.Composition.MetadataAttribute]
	public class LoggingEntityMetadataAttribute
		: Attribute
	{
		public Type LoggingType { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Attribute" /> class.</summary>
		public LoggingEntityMetadataAttribute(
			Type loggingType
		)
		{
			LoggingType = loggingType;
		}
	}
}