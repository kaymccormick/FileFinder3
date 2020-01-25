using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Util
{
	class LongObjectId : IObjectId
	{
		public object InstanceObjectId { get ; set ; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public LongObjectId (long instanceObjectId ) { InstanceObjectId = instanceObjectId ; }
	}
}
