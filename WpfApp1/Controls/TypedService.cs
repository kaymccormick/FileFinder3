using System ;
using Autofac.Core ;

namespace WpfApp1.Controls
{
	public class TypedService : Service
	{
		/// <summary>Gets a human-readable description of the service.</summary>
		/// <value>The description.</value>
		public override string Description => Desc ;

		public string Desc { get ; set ; }

		public Type ServiceType { get ; set ; }
	}
}