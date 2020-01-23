using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows ;
using NLog ;

namespace WpfApp1.Events
{
	public static class AttachedEvents
	{
		public delegate void LoggerRegisteredEventHandler(
			object                            sender,
			LoggerRegisteredEventArgs e);

	}

	public class LoggerRegisteredEventArgs : RoutedEventArgs
	{
		public ILogger Logger { get ; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.RoutedEventArgs" /> class, using the supplied routed event identifier, and providing the opportunity to declare a different source for the event.</summary>
		/// <param name="routedEvent">The routed event identifier for this instance of the <see cref="T:System.Windows.RoutedEventArgs" /> class.</param>
		/// <param name="source">An alternate source that will be reported when the event is handled. This pre-populates the <see cref="P:System.Windows.RoutedEventArgs.Source" /> property.</param>
		public LoggerRegisteredEventArgs ( RoutedEvent routedEvent , object source , ILogger logger ) : base ( routedEvent , source )
		{
			this.Logger = logger ;
		}
	}
}
