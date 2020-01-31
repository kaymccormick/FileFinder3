using System.Collections.Generic ;
using System.Diagnostics ;
using System.IO ;

namespace FileFinder3
{
	public class DirectoryObjectInfo : ObjectInfo
	{
		public IDictionary < string , ObjectInfo > Children =
			new Dictionary < string , ObjectInfo > ( ) ;

		public IDictionary < string , ExtensionInfo > Extensions =
			new Dictionary < string , ExtensionInfo > ( ) ;

		public DirectoryObjectInfo ( DirectoryInfo d ) : base ( d ) { }

		public DirectoryObjectInfo ( ) { }

		public SummaryInfo SummaryInfo { get ; set ; } = new SummaryInfo ( ) ;

		private ObjectInfo this [ string component ]
		{
			get => Children[ component ] ;
			set => Children[ component ] = value ;
		}

		public void AddChild ( ObjectInfo doi )
		{
			Debug.Assert ( ! Children.ContainsKey ( doi.Component ) ) ;
			Children[ doi.Component ] = doi ;
		}
	}

	public class ExtensionInfo
	{
		public List < ObjectInfo > Items       = new List < ObjectInfo > ( ) ;
		public SummaryInfo         SummaryInfo = new SummaryInfo ( ) ;
	}
}