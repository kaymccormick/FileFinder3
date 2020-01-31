namespace WpfApp1.Util
{
	internal class LongObjectId : IObjectId
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public LongObjectId ( long instanceObjectId ) { InstanceObjectId = instanceObjectId ; }

		public object InstanceObjectId { get ; set ; }
	}
}