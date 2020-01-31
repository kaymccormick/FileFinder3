using System ;

namespace KayMcCormick.Dev.Test.Metadata
{
	[ AttributeUsage (
		                 AttributeTargets.Assembly
		                 | AttributeTargets.Class
		                 | AttributeTargets.Module
	                 ) ]
	public class WpfTestApplicationAttribute : Attribute
	{
	}
}