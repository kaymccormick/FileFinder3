﻿using AppShared;
using Common.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup ;
using Xunit ;

namespace AppSharedTests
{
	public class TestObjectIdExtension
	{
		[Fact]
		public void TestObjectIdExtension1 ( )
		{
		
			AppLoggingConfigHelper.EnsureLoggingConfigured ( );
			var s = "<appShared:ObjectId             xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n             xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n             xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" \r\n             xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"\r\n             xmlns:appShared=\"clr-namespace:AppShared;assembly=AppShared\"\r\n             xmlns:common=\"clr-namespace:Common\"\r\n/>" ;
			StringReader sr = new StringReader (
			                                    s
			                                   ) ;
			var load = XamlReader.Load ( new MemoryStream ( Encoding.UTF8.GetBytes ( s ) ) ) ;
			var x = load as ObjectIdExtension ;
			
		}

	}
}