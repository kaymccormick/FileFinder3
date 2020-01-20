#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// DuplicateKeyException.cs
// 
// 2020-01-20-6:26 AM
// 
// ---

#endregion

using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace WpfApp1Tests3
{
    public class DuplicateKeyException
        : Exception
    {
        private object key;

        private static string DefaultMessage
        {
            get
            {
                return "Duplicate Key";
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class.</summary>
        public DuplicateKeyException() : this(DuplicateKeyException.DefaultMessage)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public DuplicateKeyException(
            string message
        ) : base( message )
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public DuplicateKeyException(
            string    message,
            Exception innerException
        ) : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public DuplicateKeyException(
            Exception innerException
        ) : base(DuplicateKeyException.DefaultMessage, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with serialized data.</summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="info" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is <see langword="null" /> or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
        protected DuplicateKeyException(
            [ NotNull ] SerializationInfo info,
            StreamingContext              context
        ) : base( info, context )
        {
        }

        public DuplicateKeyException(Object key)
            : this(DuplicateKeyException.DefaultMessage + ": " + key.ToString())
        {
            this.key = key;
        }
    }
}