#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// ServiceProviderProxy.cs
// 
// 2020-01-22-9:21 AM
// 
// ---
#endregion

using System ;
using System.Windows.Markup ;
using NLog ;

namespace WpfApp1Tests3
{
	internal class ServiceProviderProxy : IServiceProvider
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		/// <summary>Gets the service object of the specified type.</summary>
		/// <param name="serviceType">
		///     An object that specifies the type of service object
		///     to get.
		/// </param>
		/// <returns>
		///     A service object of type <paramref name="serviceType" />.
		///     -or-
		///     <see langword="null" /> if there is no service object of type
		///     <paramref name="serviceType" />.
		/// </returns>
		public object GetService ( Type serviceType )
		{
			Logger.Debug ( $"{nameof ( GetService )} {serviceType}" ) ;
			if ( serviceType == typeof ( IProvideValueTarget ) )
			{
				return new ProvideValueTarget ( null , null ) ;
			}

			throw new NotImplementedException ( ) ;
		}
	}
}