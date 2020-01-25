#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// LoggerImpl.cs
// 
// 2020-01-22-9:53 AM
// 
// ---

#endregion

using System;
using System.Threading.Tasks;
using NLog;

namespace WpfApp1.Logging
{
	public class LoggerImpl : ILogger
	{
		private ILogger _loggerImplementation;

		/// <summary>Writes the diagnostic message at the specified level.</summary>
		/// <param name="level">The log level.</param>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Log(
			LogLevel level,
			object   value
		)
		{
			_loggerImplementation.Log( level, value );
		}

		/// <summary>Writes the diagnostic message at the specified level.</summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			object          value
		)
		{
			_loggerImplementation.Log( level, formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified parameters.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			object   arg1,
			object   arg2
		)
		{
			_loggerImplementation.Log( level, message, arg1, arg2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified parameters.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		/// <param name="arg3">Third argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			object   arg1,
			object   arg2,
			object   arg3
		)
		{
			_loggerImplementation.Log( level, message, arg1, arg2, arg3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			bool            argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			bool     argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			char            argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			char     argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			byte            argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			byte     argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			string          argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			string   argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			int             argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			int      argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			long            argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			long     argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			float           argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			float    argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			double          argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			double   argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			decimal         argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			decimal  argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			object          argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			object   argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			sbyte           argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			sbyte    argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			uint            argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			uint     argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			ulong           argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified value as a parameter.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log(
			LogLevel level,
			string   message,
			ulong    argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Gets a value indicating whether logging is enabled for the specified level.
		/// </summary>
		/// <param name="level">Log level to be checked.</param>
		/// <returns>A value of <see langword="true" /> if logging is enabled for the specified level, otherwise it returns <see langword="false" />.</returns>
		public bool IsEnabled(
			LogLevel level
		)
		{
			return _loggerImplementation.IsEnabled( level );
		}

		/// <summary>Writes the specified diagnostic message.</summary>
		/// <param name="logEvent">Log event.</param>
		public void Log(
			LogEventInfo logEvent
		)
		{
			_loggerImplementation.Log( logEvent );
		}

		/// <summary>Writes the specified diagnostic message.</summary>
		/// <param name="wrapperType">The name of the type that wraps Logger.</param>
		/// <param name="logEvent">Log event.</param>
		public void Log(
			Type         wrapperType,
			LogEventInfo logEvent
		)
		{
			_loggerImplementation.Log( wrapperType, logEvent );
		}

		/// <overloads>
		/// Writes the diagnostic message at the specified level using the specified format provider and format parameters.
		/// </overloads>
		/// <summary>Writes the diagnostic message at the specified level.</summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="level">The log level.</param>
		/// <param name="value">The value to be written.</param>
		public void Log < T >(
			LogLevel level,
			T        value
		)
		{
			_loggerImplementation.Log( level, value );
		}

		/// <summary>Writes the diagnostic message at the specified level.</summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">The value to be written.</param>
		public void Log < T >(
			LogLevel        level,
			IFormatProvider formatProvider,
			T               value
		)
		{
			_loggerImplementation.Log( level, formatProvider, value );
		}

		/// <summary>Writes the diagnostic message at the specified level.</summary>
		/// <param name="level">The log level.</param>
		/// <param name="messageFunc">A function returning message to be written. Function is not evaluated if logging is not enabled.</param>
		public void Log(
			LogLevel            level,
			LogMessageGenerator messageFunc
		)
		{
			_loggerImplementation.Log( level, messageFunc );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the specified level.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		// public void LogException(
		// 	LogLevel  level,
		// 	string    message,
		// 	Exception exception
		// )
		// {
		// 	_loggerImplementation.LogException( level, message, exception );
		// }

		/// <summary>
		/// Writes the diagnostic message and exception at the specified level.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="args">Arguments to format.</param>
		/// <param name="exception">An exception to be logged.</param>
		public void Log(
			LogLevel        level,
			Exception       exception,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Log( level, exception, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the specified level.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="args">Arguments to format.</param>
		/// <param name="exception">An exception to be logged.</param>
		public void Log(
			LogLevel        level,
			Exception       exception,
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Log( level, exception, formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified parameters and formatting them with the supplied format provider.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Log(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, args );
		}

		/// <summary>Writes the diagnostic message at the specified level.</summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">Log message.</param>
		public void Log(
			LogLevel level,
			string   message
		)
		{
			_loggerImplementation.Log( level, message );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified parameters.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Log(
			LogLevel        level,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Log( level, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the specified level.
		/// </summary>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void Log(
			LogLevel  level,
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.Log( level, message, exception );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log < TArgument >(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			TArgument       argument
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified parameter.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Log < TArgument >(
			LogLevel  level,
			string    message,
			TArgument argument
		)
		{
			_loggerImplementation.Log( level, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Log < TArgument1, TArgument2 >(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Log < TArgument1, TArgument2 >(
			LogLevel   level,
			string     message,
			TArgument1 argument1,
			TArgument2 argument2
		)
		{
			_loggerImplementation.Log( level, message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="level">The log level.</param>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Log < TArgument1, TArgument2, TArgument3 >(
			LogLevel        level,
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2,
			TArgument3      argument3
		)
		{
			_loggerImplementation.Log( level, formatProvider, message, argument1, argument2, argument3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the specified level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="level">The log level.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Log < TArgument1, TArgument2, TArgument3 >(
			LogLevel   level,
			string     message,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3
		)
		{
			_loggerImplementation.Log( level, message, argument1, argument2, argument3 );
		}

		/// <summary>Gets the name of the logger.</summary>
		public string Name
		{
			get => _loggerImplementation.Name;
		}

		/// <summary>Gets the factory that created this logger.</summary>
		public LogFactory Factory
		{
			get => _loggerImplementation.Factory;
		}

		/// <summary>Occurs when logger configuration changes.</summary>
		public event EventHandler < EventArgs > LoggerReconfigured
		{
			add => _loggerImplementation.LoggerReconfigured += value;
			remove => _loggerImplementation.LoggerReconfigured -= value;
		}

		/// <summary>
		/// Runs the provided action. If the action throws, the exception is logged at <c>Error</c> level. The exception is not propagated outside of this method.
		/// </summary>
		/// <param name="action">Action to execute.</param>
		public void Swallow(
			Action action
		)
		{
			_loggerImplementation.Swallow( action );
		}

		/// <summary>
		/// Runs the provided function and returns its result. If an exception is thrown, it is logged at <c>Error</c> level.
		/// The exception is not propagated outside of this method; a default value is returned instead.
		/// </summary>
		/// <typeparam name="T">Return type of the provided function.</typeparam>
		/// <param name="func">Function to run.</param>
		/// <returns>Result returned by the provided function or the default value of type <typeparamref name="T" /> in case of exception.</returns>
		public T Swallow < T >(
			Func < T > func
		)
		{
			return _loggerImplementation.Swallow( func );
		}

		/// <summary>
		/// Runs the provided function and returns its result. If an exception is thrown, it is logged at <c>Error</c> level.
		/// The exception is not propagated outside of this method; a fallback value is returned instead.
		/// </summary>
		/// <typeparam name="T">Return type of the provided function.</typeparam>
		/// <param name="func">Function to run.</param>
		/// <param name="fallback">Fallback value to return in case of exception.</param>
		/// <returns>Result returned by the provided function or fallback value in case of exception.</returns>
		public T Swallow < T >(
			Func < T > func,
			T          fallback
		)
		{
			return _loggerImplementation.Swallow( func, fallback );
		}

		/// <summary>
		/// Logs an exception is logged at <c>Error</c> level if the provided task does not run to completion.
		/// </summary>
		/// <param name="task">The task for which to log an error if it does not run to completion.</param>
		/// <remarks>This method is useful in fire-and-forget situations, where application logic does not depend on completion of task. This method is avoids C# warning CS4014 in such situations.</remarks>
		public void Swallow(
			Task task
		)
		{
			_loggerImplementation.Swallow( task );
		}

		/// <summary>
		/// Returns a task that completes when a specified task to completes. If the task does not run to completion, an exception is logged at <c>Error</c> level. The returned task always runs to completion.
		/// </summary>
		/// <param name="task">The task for which to log an error if it does not run to completion.</param>
		/// <returns>A task that completes in the <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> state when <paramref name="task" /> completes.</returns>
		public Task SwallowAsync(
			Task task
		)
		{
			return _loggerImplementation.SwallowAsync( task );
		}

		/// <summary>
		/// Runs async action. If the action throws, the exception is logged at <c>Error</c> level. The exception is not propagated outside of this method.
		/// </summary>
		/// <param name="asyncAction">Async action to execute.</param>
		/// <returns>A task that completes in the <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> state when <paramref name="asyncAction" /> completes.</returns>
		public Task SwallowAsync(
			Func < Task > asyncAction
		)
		{
			return _loggerImplementation.SwallowAsync( asyncAction );
		}

		/// <summary>
		/// Runs the provided async function and returns its result. If the task does not run to completion, an exception is logged at <c>Error</c> level.
		/// The exception is not propagated outside of this method; a default value is returned instead.
		/// </summary>
		/// <typeparam name="TResult">Return type of the provided function.</typeparam>
		/// <param name="asyncFunc">Async function to run.</param>
		/// <returns>A task that represents the completion of the supplied task. If the supplied task ends in the <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> state, the result of the new task will be the result of the supplied task; otherwise, the result of the new task will be the default value of type <typeparamref name="TResult" />.</returns>
		public Task < TResult > SwallowAsync < TResult >(
			Func < Task < TResult > > asyncFunc
		)
		{
			return _loggerImplementation.SwallowAsync( asyncFunc );
		}

		/// <summary>
		/// Runs the provided async function and returns its result. If the task does not run to completion, an exception is logged at <c>Error</c> level.
		/// The exception is not propagated outside of this method; a fallback value is returned instead.
		/// </summary>
		/// <typeparam name="TResult">Return type of the provided function.</typeparam>
		/// <param name="asyncFunc">Async function to run.</param>
		/// <param name="fallback">Fallback value to return if the task does not end in the <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> state.</param>
		/// <returns>A task that represents the completion of the supplied task. If the supplied task ends in the <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> state, the result of the new task will be the result of the supplied task; otherwise, the result of the new task will be the fallback value.</returns>
		public Task < TResult > SwallowAsync < TResult >(
			Func < Task < TResult > > asyncFunc,
			TResult                   fallback
		)
		{
			return _loggerImplementation.SwallowAsync( asyncFunc, fallback );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level.
		/// </summary>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Trace(
			object value
		)
		{
			_loggerImplementation.Trace( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Trace(
			IFormatProvider formatProvider,
			object          value
		)
		{
			_loggerImplementation.Trace( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		public void Trace(
			string message,
			object arg1,
			object arg2
		)
		{
			_loggerImplementation.Trace( message, arg1, arg2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		/// <param name="arg3">Third argument to format.</param>
		public void Trace(
			string message,
			object arg1,
			object arg2,
			object arg3
		)
		{
			_loggerImplementation.Trace( message, arg1, arg2, arg3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			bool            argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			bool   argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			char            argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			char   argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			byte            argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			byte   argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			string          argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			string argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			int             argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			int    argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			long            argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			long   argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			float           argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			float  argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			double          argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			double argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			decimal         argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string  message,
			decimal argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			object          argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			object argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		/// s
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			sbyte           argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			sbyte  argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			uint            argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			uint   argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			ulong           argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace(
			string message,
			ulong  argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level.
		/// </summary>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Debug(
			object value
		)
		{
			_loggerImplementation.Debug( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Debug(
			IFormatProvider formatProvider,
			object          value
		)
		{
			_loggerImplementation.Debug( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		public void Debug(
			string message,
			object arg1,
			object arg2
		)
		{
			_loggerImplementation.Debug( message, arg1, arg2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		/// <param name="arg3">Third argument to format.</param>
		public void Debug(
			string message,
			object arg1,
			object arg2,
			object arg3
		)
		{
			_loggerImplementation.Debug( message, arg1, arg2, arg3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			bool            argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			bool   argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			char            argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			char   argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			byte            argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			byte   argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			string          argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			string argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			int             argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			int    argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			long            argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			long   argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			float           argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			float  argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			double          argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			double argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			decimal         argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string  message,
			decimal argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			object          argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			object argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			sbyte           argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			sbyte  argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			uint            argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			uint   argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			ulong           argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug(
			string message,
			ulong  argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level.
		/// </summary>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Info(
			object value
		)
		{
			_loggerImplementation.Info( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Info(
			IFormatProvider formatProvider,
			object          value
		)
		{
			_loggerImplementation.Info( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		public void Info(
			string message,
			object arg1,
			object arg2
		)
		{
			_loggerImplementation.Info( message, arg1, arg2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		/// <param name="arg3">Third argument to format.</param>
		public void Info(
			string message,
			object arg1,
			object arg2,
			object arg3
		)
		{
			_loggerImplementation.Info( message, arg1, arg2, arg3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			bool            argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			bool   argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			char            argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			char   argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			byte            argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			byte   argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			string          argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			string argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			int             argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			int    argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			long            argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			long   argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			float           argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			float  argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			double          argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			double argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			decimal         argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string  message,
			decimal argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			object          argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			object argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			sbyte           argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			sbyte  argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			uint            argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			uint   argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			ulong           argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info(
			string message,
			ulong  argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level.
		/// </summary>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Warn(
			object value
		)
		{
			_loggerImplementation.Warn( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Warn(
			IFormatProvider formatProvider,
			object          value
		)
		{
			_loggerImplementation.Warn( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		public void Warn(
			string message,
			object arg1,
			object arg2
		)
		{
			_loggerImplementation.Warn( message, arg1, arg2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		/// <param name="arg3">Third argument to format.</param>
		public void Warn(
			string message,
			object arg1,
			object arg2,
			object arg3
		)
		{
			_loggerImplementation.Warn( message, arg1, arg2, arg3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			bool            argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			bool   argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			char            argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			char   argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			byte            argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			byte   argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			string          argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			string argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			int             argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			int    argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			long            argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			long   argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			float           argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			float  argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			double          argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			double argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			decimal         argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string  message,
			decimal argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			object          argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			object argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			sbyte           argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			sbyte  argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			uint            argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			uint   argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			ulong           argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn(
			string message,
			ulong  argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level.
		/// </summary>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Error(
			object value
		)
		{
			_loggerImplementation.Error( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Error(
			IFormatProvider formatProvider,
			object          value
		)
		{
			_loggerImplementation.Error( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		public void Error(
			string message,
			object arg1,
			object arg2
		)
		{
			_loggerImplementation.Error( message, arg1, arg2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		/// <param name="arg3">Third argument to format.</param>
		public void Error(
			string message,
			object arg1,
			object arg2,
			object arg3
		)
		{
			_loggerImplementation.Error( message, arg1, arg2, arg3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			bool            argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			bool   argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			char            argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			char   argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			byte            argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			byte   argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			string          argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			string argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			int             argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			int    argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			long            argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			long   argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			float           argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			float  argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			double          argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			double argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			decimal         argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string  message,
			decimal argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			object          argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			object argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			sbyte           argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			sbyte  argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			uint            argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			uint   argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			ulong           argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error(
			string message,
			ulong  argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level.
		/// </summary>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Fatal(
			object value
		)
		{
			_loggerImplementation.Fatal( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">A <see langword="object" /> to be written.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			object          value
		)
		{
			_loggerImplementation.Fatal( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		public void Fatal(
			string message,
			object arg1,
			object arg2
		)
		{
			_loggerImplementation.Fatal( message, arg1, arg2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="arg1">First argument to format.</param>
		/// <param name="arg2">Second argument to format.</param>
		/// <param name="arg3">Third argument to format.</param>
		public void Fatal(
			string message,
			object arg1,
			object arg2,
			object arg3
		)
		{
			_loggerImplementation.Fatal( message, arg1, arg2, arg3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			bool            argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			bool   argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			char            argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			char   argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			byte            argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			byte   argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			string          argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			string argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			int             argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			int    argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			long            argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			long   argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			float           argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			float  argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			double          argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			double argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			decimal         argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string  message,
			decimal argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			object          argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			object argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			sbyte           argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			sbyte  argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			uint            argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			uint   argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			ulong           argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified value as a parameter.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal(
			string message,
			ulong  argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <overloads>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified format provider and format parameters.
		/// </overloads>
		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="value">The value to be written.</param>
		public void Trace < T >(
			T value
		)
		{
			_loggerImplementation.Trace( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">The value to be written.</param>
		public void Trace < T >(
			IFormatProvider formatProvider,
			T               value
		)
		{
			_loggerImplementation.Trace( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level.
		/// </summary>
		/// <param name="messageFunc">A function returning message to be written. Function is not evaluated if logging is not enabled.</param>
		public void Trace(
			LogMessageGenerator messageFunc
		)
		{
			_loggerImplementation.Trace( messageFunc );
		}

#if EXCEPTIONMETHODS
		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Trace</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void TraceException(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.TraceException( message, exception );
		}
#endif

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Trace</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		public void Trace(
			Exception exception,
			string    message
		)
		{
			_loggerImplementation.Trace( exception, message );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Trace</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Trace(
			Exception       exception,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Trace( exception, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Trace</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Trace(
			Exception       exception,
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Trace( exception, formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified parameters and formatting them with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Trace(
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Trace( formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void Trace(
			string message
		)
		{
			_loggerImplementation.Trace( message );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Trace(
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Trace( message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Trace</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void Trace(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.Trace( message, exception );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace < TArgument >(
			IFormatProvider formatProvider,
			string          message,
			TArgument       argument
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified parameter.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Trace < TArgument >(
			string    message,
			TArgument argument
		)
		{
			_loggerImplementation.Trace( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Trace < TArgument1, TArgument2 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Trace < TArgument1, TArgument2 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2
		)
		{
			_loggerImplementation.Trace( message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Trace < TArgument1, TArgument2, TArgument3 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2,
			TArgument3      argument3
		)
		{
			_loggerImplementation.Trace( formatProvider, message, argument1, argument2, argument3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Trace</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Trace < TArgument1, TArgument2, TArgument3 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3
		)
		{
			_loggerImplementation.Trace( message, argument1, argument2, argument3 );
		}

		/// <overloads>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified format provider and format parameters.
		/// </overloads>
		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="value">The value to be written.</param>
		public void Debug < T >(
			T value
		)
		{
			_loggerImplementation.Debug( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">The value to be written.</param>
		public void Debug < T >(
			IFormatProvider formatProvider,
			T               value
		)
		{
			_loggerImplementation.Debug( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level.
		/// </summary>
		/// <param name="messageFunc">A function returning message to be written. Function is not evaluated if logging is not enabled.</param>
		public void Debug(
			LogMessageGenerator messageFunc
		)
		{
			_loggerImplementation.Debug( messageFunc );
		}
#if EXCEPTIONMETHODS
		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Debug</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void DebugException(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.DebugException( message, exception );
		}

#endif
		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Debug</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		public void Debug(
			Exception exception,
			string    message
		)
		{
			_loggerImplementation.Debug( exception, message );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Debug</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Debug(
			Exception       exception,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Debug( exception, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Debug</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Debug(
			Exception       exception,
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Debug( exception, formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified parameters and formatting them with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Debug(
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Debug( formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void Debug(
			string message
		)
		{
			_loggerImplementation.Debug( message );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Debug(
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Debug( message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Debug</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void Debug(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.Debug( message, exception );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug < TArgument >(
			IFormatProvider formatProvider,
			string          message,
			TArgument       argument
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified parameter.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Debug < TArgument >(
			string    message,
			TArgument argument
		)
		{
			_loggerImplementation.Debug( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Debug < TArgument1, TArgument2 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Debug < TArgument1, TArgument2 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2
		)
		{
			_loggerImplementation.Debug( message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Debug < TArgument1, TArgument2, TArgument3 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2,
			TArgument3      argument3
		)
		{
			_loggerImplementation.Debug( formatProvider, message, argument1, argument2, argument3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Debug</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Debug < TArgument1, TArgument2, TArgument3 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3
		)
		{
			_loggerImplementation.Debug( message, argument1, argument2, argument3 );
		}

		/// <overloads>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified format provider and format parameters.
		/// </overloads>
		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="value">The value to be written.</param>
		public void Info < T >(
			T value
		)
		{
			_loggerImplementation.Info( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">The value to be written.</param>
		public void Info < T >(
			IFormatProvider formatProvider,
			T               value
		)
		{
			_loggerImplementation.Info( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level.
		/// </summary>
		/// <param name="messageFunc">A function returning message to be written. Function is not evaluated if logging is not enabled.</param>
		public void Info(
			LogMessageGenerator messageFunc
		)
		{
			_loggerImplementation.Info( messageFunc );
		}

#if EXCEPTIONMETHODS
		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Info</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void InfoException(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.InfoException( message, exception );
		}
#endif
		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Info</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		public void Info(
			Exception exception,
			string    message
		)
		{
			_loggerImplementation.Info( exception, message );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Info</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Info(
			Exception       exception,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Info( exception, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Info</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Info(
			Exception       exception,
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Info( exception, formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified parameters and formatting them with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Info(
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Info( formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void Info(
			string message
		)
		{
			_loggerImplementation.Info( message );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Info(
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Info( message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Info</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void Info(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.Info( message, exception );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info < TArgument >(
			IFormatProvider formatProvider,
			string          message,
			TArgument       argument
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified parameter.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Info < TArgument >(
			string    message,
			TArgument argument
		)
		{
			_loggerImplementation.Info( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Info < TArgument1, TArgument2 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Info < TArgument1, TArgument2 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2
		)
		{
			_loggerImplementation.Info( message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Info < TArgument1, TArgument2, TArgument3 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2,
			TArgument3      argument3
		)
		{
			_loggerImplementation.Info( formatProvider, message, argument1, argument2, argument3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Info</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Info < TArgument1, TArgument2, TArgument3 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3
		)
		{
			_loggerImplementation.Info( message, argument1, argument2, argument3 );
		}

		/// <overloads>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified format provider and format parameters.
		/// </overloads>
		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="value">The value to be written.</param>
		public void Warn < T >(
			T value
		)
		{
			_loggerImplementation.Warn( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">The value to be written.</param>
		public void Warn < T >(
			IFormatProvider formatProvider,
			T               value
		)
		{
			_loggerImplementation.Warn( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level.
		/// </summary>
		/// <param name="messageFunc">A function returning message to be written. Function is not evaluated if logging is not enabled.</param>
		public void Warn(
			LogMessageGenerator messageFunc
		)
		{
			_loggerImplementation.Warn( messageFunc );
		}
#if EXCEPTIONMETHODS

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Warn</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void WarnException(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.WarnException( message, exception );
		}
#endif

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Warn</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		public void Warn(
			Exception exception,
			string    message
		)
		{
			_loggerImplementation.Warn( exception, message );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Warn</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Warn(
			Exception       exception,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Warn( exception, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Warn</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Warn(
			Exception       exception,
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Warn( exception, formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified parameters and formatting them with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Warn(
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Warn( formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void Warn(
			string message
		)
		{
			_loggerImplementation.Warn( message );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Warn(
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Warn( message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Warn</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void Warn(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.Warn( message, exception );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn < TArgument >(
			IFormatProvider formatProvider,
			string          message,
			TArgument       argument
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified parameter.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Warn < TArgument >(
			string    message,
			TArgument argument
		)
		{
			_loggerImplementation.Warn( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Warn < TArgument1, TArgument2 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Warn < TArgument1, TArgument2 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2
		)
		{
			_loggerImplementation.Warn( message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Warn < TArgument1, TArgument2, TArgument3 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2,
			TArgument3      argument3
		)
		{
			_loggerImplementation.Warn( formatProvider, message, argument1, argument2, argument3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Warn</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Warn < TArgument1, TArgument2, TArgument3 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3
		)
		{
			_loggerImplementation.Warn( message, argument1, argument2, argument3 );
		}

		/// <overloads>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified format provider and format parameters.
		/// </overloads>
		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="value">The value to be written.</param>
		public void Error < T >(
			T value
		)
		{
			_loggerImplementation.Error( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">The value to be written.</param>
		public void Error < T >(
			IFormatProvider formatProvider,
			T               value
		)
		{
			_loggerImplementation.Error( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level.
		/// </summary>
		/// <param name="messageFunc">A function returning message to be written. Function is not evaluated if logging is not enabled.</param>
		public void Error(
			LogMessageGenerator messageFunc
		)
		{
			_loggerImplementation.Error( messageFunc );
		}
#if EXCEPTIONMETHODS
		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Error</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void ErrorException(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.ErrorException( message, exception );
		}
#endif

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Error</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		public void Error(
			Exception exception,
			string    message
		)
		{
			_loggerImplementation.Error( exception, message );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Error</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Error(
			Exception       exception,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Error( exception, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Error</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Error(
			Exception       exception,
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Error( exception, formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified parameters and formatting them with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Error(
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Error( formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void Error(
			string message
		)
		{
			_loggerImplementation.Error( message );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Error(
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Error( message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Error</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void Error(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.Error( message, exception );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error < TArgument >(
			IFormatProvider formatProvider,
			string          message,
			TArgument       argument
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified parameter.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Error < TArgument >(
			string    message,
			TArgument argument
		)
		{
			_loggerImplementation.Error( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Error < TArgument1, TArgument2 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Error < TArgument1, TArgument2 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2
		)
		{
			_loggerImplementation.Error( message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Error < TArgument1, TArgument2, TArgument3 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2,
			TArgument3      argument3
		)
		{
			_loggerImplementation.Error( formatProvider, message, argument1, argument2, argument3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Error</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Error < TArgument1, TArgument2, TArgument3 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3
		)
		{
			_loggerImplementation.Error( message, argument1, argument2, argument3 );
		}

		/// <overloads>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified format provider and format parameters.
		/// </overloads>
		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="value">The value to be written.</param>
		public void Fatal < T >(
			T value
		)
		{
			_loggerImplementation.Fatal( value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="value">The value to be written.</param>
		public void Fatal < T >(
			IFormatProvider formatProvider,
			T               value
		)
		{
			_loggerImplementation.Fatal( formatProvider, value );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level.
		/// </summary>
		/// <param name="messageFunc">A function returning message to be written. Function is not evaluated if logging is not enabled.</param>
		public void Fatal(
			LogMessageGenerator messageFunc
		)
		{
			_loggerImplementation.Fatal( messageFunc );
		}
#if EXCEPTIONMETHODS
		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Fatal</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void FatalException(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.FatalException( message, exception );
		}
#endif
		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Fatal</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		public void Fatal(
			Exception exception,
			string    message
		)
		{
			_loggerImplementation.Fatal( exception, message );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Fatal</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Fatal(
			Exception       exception,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Fatal( exception, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Fatal</c> level.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <param name="args">Arguments to format.</param>
		public void Fatal(
			Exception       exception,
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Fatal( exception, formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters and formatting them with the supplied format provider.
		/// </summary>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Fatal(
			IFormatProvider formatProvider,
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, args );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level.
		/// </summary>
		/// <param name="message">Log message.</param>
		public void Fatal(
			string message
		)
		{
			_loggerImplementation.Fatal( message );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters.
		/// </summary>
		/// <param name="message">A <see langword="string" /> containing format items.</param>
		/// <param name="args">Arguments to format.</param>
		public void Fatal(
			string          message,
			params object[] args
		)
		{
			_loggerImplementation.Fatal( message, args );
		}

		/// <summary>
		/// Writes the diagnostic message and exception at the <c>Fatal</c> level.
		/// </summary>
		/// <param name="message">A <see langword="string" /> to be written.</param>
		/// <param name="exception">An exception to be logged.</param>
		/// <remarks>This method was marked as obsolete before NLog 4.3.11 and it may be removed in a future release.</remarks>
		public void Fatal(
			string    message,
			Exception exception
		)
		{
			_loggerImplementation.Fatal( message, exception );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameter and formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal < TArgument >(
			IFormatProvider formatProvider,
			string          message,
			TArgument       argument
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameter.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument">The argument to format.</param>
		public void Fatal < TArgument >(
			string    message,
			TArgument argument
		)
		{
			_loggerImplementation.Fatal( message, argument );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Fatal < TArgument1, TArgument2 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		public void Fatal < TArgument1, TArgument2 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2
		)
		{
			_loggerImplementation.Fatal( message, argument1, argument2 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified arguments formatting it with the supplied format provider.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Fatal < TArgument1, TArgument2, TArgument3 >(
			IFormatProvider formatProvider,
			string          message,
			TArgument1      argument1,
			TArgument2      argument2,
			TArgument3      argument3
		)
		{
			_loggerImplementation.Fatal( formatProvider, message, argument1, argument2, argument3 );
		}

		/// <summary>
		/// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters.
		/// </summary>
		/// <typeparam name="TArgument1">The type of the first argument.</typeparam>
		/// <typeparam name="TArgument2">The type of the second argument.</typeparam>
		/// <typeparam name="TArgument3">The type of the third argument.</typeparam>
		/// <param name="message">A <see langword="string" /> containing one format item.</param>
		/// <param name="argument1">The first argument to format.</param>
		/// <param name="argument2">The second argument to format.</param>
		/// <param name="argument3">The third argument to format.</param>
		public void Fatal < TArgument1, TArgument2, TArgument3 >(
			string     message,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3
		)
		{
			_loggerImplementation.Fatal( message, argument1, argument2, argument3 );
		}

		/// <summary>
		/// Gets a value indicating whether logging is enabled for the <c>Trace</c> level.
		/// </summary>
		/// <returns>A value of <see langword="true" /> if logging is enabled for the <c>Trace</c> level, otherwise it returns <see langword="false" />.</returns>
		public bool IsTraceEnabled
		{
			get => _loggerImplementation.IsTraceEnabled;
		}

		/// <summary>
		/// Gets a value indicating whether logging is enabled for the <c>Debug</c> level.
		/// </summary>
		/// <returns>A value of <see langword="true" /> if logging is enabled for the <c>Debug</c> level, otherwise it returns <see langword="false" />.</returns>
		public bool IsDebugEnabled
		{
			get => _loggerImplementation.IsDebugEnabled;
		}

		/// <summary>
		/// Gets a value indicating whether logging is enabled for the <c>Info</c> level.
		/// </summary>
		/// <returns>A value of <see langword="true" /> if logging is enabled for the <c>Info</c> level, otherwise it returns <see langword="false" />.</returns>
		public bool IsInfoEnabled
		{
			get => _loggerImplementation.IsInfoEnabled;
		}

		/// <summary>
		/// Gets a value indicating whether logging is enabled for the <c>Warn</c> level.
		/// </summary>
		/// <returns>A value of <see langword="true" /> if logging is enabled for the <c>Warn</c> level, otherwise it returns <see langword="false" />.</returns>
		public bool IsWarnEnabled
		{
			get => _loggerImplementation.IsWarnEnabled;
		}

		/// <summary>
		/// Gets a value indicating whether logging is enabled for the <c>Error</c> level.
		/// </summary>
		/// <returns>A value of <see langword="true" /> if logging is enabled for the <c>Error</c> level, otherwise it returns <see langword="false" />.</returns>
		public bool IsErrorEnabled
		{
			get => _loggerImplementation.IsErrorEnabled;
		}

		/// <summary>
		/// Gets a value indicating whether logging is enabled for the <c>Fatal</c> level.
		/// </summary>
		/// <returns>A value of <see langword="true" /> if logging is enabled for the <c>Fatal</c> level, otherwise it returns <see langword="false" />.</returns>
		public bool IsFatalEnabled
		{
			get => _loggerImplementation.IsFatalEnabled;
		}
	}
}


