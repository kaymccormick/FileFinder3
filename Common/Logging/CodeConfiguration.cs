﻿using System.Collections.Generic ;
using NLog ;
using NLog.Config ;

namespace Common.Logging
{
	public class CodeConfiguration : LoggingConfiguration
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NLog.Config.LoggingConfiguration" /> class.
		/// </summary>
		public CodeConfiguration ( ) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NLog.Config.LoggingConfiguration" /> class.
		/// </summary>
		public CodeConfiguration ( LogFactory logFactory ) : base ( logFactory )
		{
		}
	}
}
