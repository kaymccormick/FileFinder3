﻿using System.Windows ;
using NLog ;

namespace WpfApp1.Events
{
	public static class AttachedEvents
	{
		public delegate void LoggerRegisteredEventHandler (
			object                    sender
		  , LoggerRegisteredEventArgs e
		) ;
	}

	/// <summary></summary>
	/// <seealso cref="System.Windows.RoutedEventArgs" />
	/// <autogeneratedoc />
	/// TODO Edit XML Comment Template for LoggerRegisteredEventArgs
	public class LoggerRegisteredEventArgs : RoutedEventArgs
	{

		/// <summary>Initializes a new instance of the <see cref="LoggerRegisteredEventArgs"/> class.</summary>
		/// <param name="routedEvent">The routed event.</param>
		/// <param name="source">The source.</param>
		/// <param name="logger">The logger.</param>
		/// <autogeneratedoc />
		/// TODO Edit XML Comment Template for #ctor
		public LoggerRegisteredEventArgs (
			RoutedEvent routedEvent
		  , object      source
		  , ILogger     logger
		) : base ( routedEvent , source )
		{
			Logger = logger ;
		}

		/// <summary>Gets the logger.</summary>
		/// <value>The logger.</value>
		/// <autogeneratedoc />
		/// TODO Edit XML Comment Template for Logger
		public ILogger Logger { get ; }
	}
}