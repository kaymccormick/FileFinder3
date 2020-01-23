#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1
// MyActivator.cs
// 
// 2020-01-23-3:35 AM
// 
// ---
#endregion
using System ;
using System.Collections.Generic ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Activators.Delegate ;

namespace WpfApp1.Util
{
	public class MyActivator : DelegateActivator
	{
		/// <summary>
		///     Initializes a new instance of the
		///     <see cref="T:Autofac.Core.Activators.Delegate.DelegateActivator" /> class.
		/// </summary>
		/// <param name="limitType">
		///     The most specific type to which activated instances can
		///     be cast.
		/// </param>
		/// <param name="activationFunction">Activation delegate.</param>
		public MyActivator (
			Type                                                            limitType
		  , Func < IComponentContext , IEnumerable < Parameter > , object > activationFunction
		) : base ( limitType , activationFunction )
		{
		}
	}
}