using System ;
using System.Collections.Generic ;
using System.ComponentModel ;
using System.Windows.Markup ;
using Autofac ;
using Common.Converters ;
using NLog ;

namespace Common
{
	[ ContentProperty ( "Content" ) ]
	public class TestO : IAddChild

	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public TestO ( ) { Logger.Debug ( "constructeD" ) ; }

		[ TypeConverter ( typeof ( RegistrationSourceConverter ) ) ]
		public List < object > Content { get ; set ; } = new List < object > ( ) ;

		public ILifetimeScope LifetimeScope { get ; set ; }

		/// <summary>Adds a child object.</summary>
		/// <param name="value">The child object to add.</param>
		public void AddChild ( object value )
		{
			Content.Add ( value ) ;
			Logger.Info ( "${value}" ) ;
			// throw new System.NotImplementedException ( ) ;
		}

		/// <summary>Adds the text content of a node to the object.</summary>
		/// <param name="text">The text to add to the object.</param>
		public void AddText ( string text ) { throw new NotImplementedException ( ) ; }
	}
}