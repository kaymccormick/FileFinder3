﻿#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// Common
// AppTraceLisener2.cs
// 
// 2020-01-28-1:15 AM
// 
// ---
#endregion
using System ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.IO ;
using System.Linq ;
using System.Text ;
using System.Text.RegularExpressions ;
using System.Windows ;
using System.Windows.Controls ;
using System.Xaml ;
using System.Xml.Serialization ;
using AppShared ;
using NLog ;

namespace Common.Tracing
{
	public class AppTraceLisener2 : TraceListener
	{
		private static Logger         Logger = LogManager.GetCurrentClassLogger ( ) ;
		private        NLogTextWriter _nLogTextWriter ;
		public AppTraceLisener2 ( ) { _nLogTextWriter = new NLogTextWriter ( Logger ) ; }

		/// <summary>Writes trace and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		public override void TraceEvent (
			TraceEventCache eventCache
		  , string          source
		  , TraceEventType  eventType
		  , int             id
		)
		{
			var i = 0 ;
			foreach ( var q in eventCache.LogicalOperationStack )
			{
				Logger.Error ( $"{id}{i}: {source} {q.GetType ( )}: {q}" ) ;
				i ++ ;
			}
		}

		/// <summary>Writes trace information, a message, and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		public override void TraceEvent (
			TraceEventCache eventCache
		  , string          source
		  , TraceEventType  eventType
		  , int             id
		  , string          message
		)
		{
			base.TraceEvent (
			                 eventCache
			               , source
			               , eventType
			               , id
			               , message
			                ) ;
		}

		/// <summary>Writes trace information, a formatted array of objects and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		public override void TraceEvent (
			TraceEventCache eventCache
		  , string          source
		  , TraceEventType  eventType
		  , int             id
		  , string          format
		  , params object[] args
		)
		{
			var match = Regex.Match (
			                         format
			                       , "Cannot retrieve value using the binding and no valid fallback value exists; using default instead. BindingExpression:(.*)"
			                        ) ;
			if ( match != null
			     && match.Success )
			{
				var captureCollection = match.Groups[ 1 ].Captures ;
				var value = captureCollection[ 0 ].Value ;
				var strings = Regex.Split ( value , "; *" ) ;
				Dictionary < string , string > di = new Dictionary < string , string > ( ) ;
				bool isListView = false ;
				foreach ( var s in strings )
				{
					// s = s.Trim ( ) ;
					var match1 = Regex.Match ( s , "target element is (.*)" ) ;
					if ( match1 != null
					     && match1.Success )
					{
						var value1 = match1.Groups[ 1 ].Captures[ 0 ].Value ;
						if ( Regex.IsMatch ( value1 , "ListView" ) )

						{
							isListView = true ;

						}

						// Logger.Warn ( $"TARGET ELEMENT {isListView} {value1}" ) ;
					}

					Regex r = new Regex ( "= *" ) ;
					var enumerable = r.Split ( s , 2 ) ;
					if ( enumerable           != null
					     && enumerable.Length >= 2 )
					{
						var key = enumerable[ 0 ] ;
						var val = enumerable[ 1 ] ;
						di[ key ] = val ;
						// Logger.Debug ( $"{key} = {val}" ) ;
					}
					else
					{
						// Logger.Info ( s ) ;
					}
				}

				if ( isListView )
				{
					Logger.Debug (
					              String.Join (
					                           " / "
					                         , di.Select (
					                                      ( pair , i )
						                                      => $"{pair.Key} = {pair.Value}"
					                                     )
					                          )
					             ) ;
				}
			}

			var message = String.Join ( "-", eventCache.LogicalOperationStack.ToArray().Select(( o , i ) => o.ToString())) ;
			// "Path=Tag; DataItem=null; target element is 'TextBlock' (Name=''); target property is 'Text' (type 'String')"
			var d = new SerializableDictionary < string , object > ( ) ;
			var xmlDict = new Dictionary < string , string > ( ) ;
			// Logger.Info ( args.Length ) ;
			Logger.Debug ("x: " + message +  String.Join ( ", " , args ) ) ;

		}


		public Dictionary <string, Info> RoutedEvents { get ; set ; } = new Dictionary < string , Info > ();

		/// <summary>When overridden in a derived class, writes the specified message to the listener you create in the derived class.</summary>
		/// <param name="message">A message to write.</param>
		public override void Write ( string message ) { Logger.Debug ( message ) ; }

		/// <summary>When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.</summary>
		/// <param name="message">A message to write.</param>
		public override void WriteLine ( string message ) { Logger.Debug ( message ) ; }
	}
}