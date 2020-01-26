﻿using System ;
using System.Collections.Generic ;
using System.Globalization ;
using System.Linq ;
using System.Linq.Dynamic ;
using System.Text ;
using System.Threading.Tasks ;
using System.Windows.Data ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Registration ;


namespace WpfApp1.Xaml
{
	internal class RegistrationConverter : IValueConverter
	{
		private readonly ILifetimeScope    _appContainer ;
		private readonly IObjectIdProvider _provider ;

		public RegistrationConverter ( ILifetimeScope appContainer , IObjectIdProvider provider )
		{
			_appContainer = appContainer ;
			_provider     = provider ;
		}

		/// <summary>Converts a value. </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
		public object Convert (
			object      value
		  , Type        targetType
		  , object      parameter
		  , CultureInfo culture
		)
		{
			var componentRegistration = value as
				                            IComponentRegistration ;
			var instanceInfo = _provider.GetInstanceByComponentRegistration (
			                                                                 componentRegistration
			                                                                )
			                            .Select (
			                                     ( o , i ) => {
				                                     var objId = _provider.ProvideObjectInstanceIdentifier ( o, componentRegistration, o.Parameters) ;
				                                     var instanceRegistration = new InstanceRegistration (
				                                                                                          o.Instance
				                                                                                        , objId,
																										  o
				                                                                                         ) ;
				                                     return instanceRegistration ;
			                                     }
			                                    ) ;

			if ( parameter is string xx )
			{
				if ( string.Compare ( xx , "Count" ) == 0 )
				{
					return instanceInfo.Count ( ) ;
				}
			}

			return instanceInfo ;
			// IComponentRegistration x = new ComponentRegistration();
			// foreach(var svc in x.Services)
			// {
			// if(svc is TypedService ts)
			// {
			// var q =  scope.ResolveComponent(.)
			// ts.ServiceType}
			// x.Resolve

			// throw new NotImplementedException ( ) ;
		}

		/// <summary>Converts a value. </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
		public object ConvertBack (
			object      value
		  , Type        targetType
		  , object      parameter
		  , CultureInfo culture
		)
		{
			throw new NotImplementedException ( ) ;
		}
	}
}