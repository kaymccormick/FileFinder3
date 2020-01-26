﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Util
{
	public static class DebugHelper
	{
		public static string ShortenGuid ( this Guid guid )
		{
			var shortenGuidba = guid.ToByteArray ( ) ;
			return $"{shortenGuidba[ 3 ]:x2}{shortenGuidba[ 2 ]:x2}..{shortenGuidba[ 15 ]:x2}" ; 
		}
		public static string ToShortGuid(this Guid newGuid)
		{
			string modifiedBase64 = Convert.ToBase64String(newGuid.ToByteArray())
			                               .Replace('+', '-').Replace('/', '_') // avoid invalid URL characters
			                               .Substring(0, 22);
			return modifiedBase64;
		}

		public static Guid ParseShortGuid(string shortGuid)
		{
			string base64 = shortGuid.Replace('-', '+').Replace('_', '/') + "==";
			Byte[] bytes = Convert.FromBase64String(base64);
			return new Guid(bytes);
		} 
	}
}