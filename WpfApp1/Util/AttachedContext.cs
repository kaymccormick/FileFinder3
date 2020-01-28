#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// AttachedContext.cs
// 
// 2020-01-22-9:21 AM
// 
// ---

#endregion

using System ;
using System.Linq.Dynamic ;
using System.Runtime.Serialization ;
using Common ;
using Common.Context ;
using DynamicData.Annotations ;

namespace WpfApp1.Util
{
	public class AttachedContext : IDisposable
	{
		private readonly InfoContext                  _infoContext;
		private readonly ContextStack < InfoContext > contextStack;

		public AttachedContext(
			 ContextStack < InfoContext > contextStack,
			InfoContext                  context
		)
		{
			this.contextStack = contextStack ?? throw new ArgumentNullException ( nameof ( contextStack ) );
			_infoContext      = context;
			contextStack.Push( _infoContext );
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing,
		///     or resetting unmanaged resources.
		/// </summary>
		public void Dispose ()
		{
			Dispose (  true ) ;
			GC.SuppressFinalize(this);

		}

		protected virtual void Dispose ( bool b )
		{
			if ( ! b )
			{
				return ;
			}
			if(!contextStack.Any())
			{
				throw new ContectStsackException ( "Empty stack - expected at least one elmeent" ) ;
			}
			//Assert.NotEmpty ( contextStack ) ;
			if ( ! contextStack.Peek ( ).Equals ( _infoContext ) )
			{
				throw new ContectStsackException("");
			}
			//Assert.True ( ReferenceEquals ( _infoContext , contextStack.First ( ) ) ) ;
			contextStack.Pop ( ) ;
		}

	}

	public class ContectStsackException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class.</summary>
		public ContectStsackException ( ) {
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		public ContectStsackException ( string message ) : base ( message )
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		public ContectStsackException ( string message , Exception innerException ) : base ( message , innerException )
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is <see langword="null" /> or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
		protected ContectStsackException ( [ NotNull ] SerializationInfo info , StreamingContext context ) : base ( info , context )
		{
		}
	}

}