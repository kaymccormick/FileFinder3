using System;
using System.Collections.Generic;
using System.IO ;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup ;
using AppShared ;
using Common.Context ;
using Common.Logging ;
using NLog ;
using NLog.Fluent ;
using Xunit ;

namespace AppSharedTests
{
	public class Class1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public Class1 ( ) {
			InstanceFactory = new Factory(null);
			MyStack = InstanceFactory.CreateContextStack < InfoContext > ( ) ;
		}

		public ContextStack<InfoContext> MyStack
			{ get ; set ; }

		public Factory InstanceFactory { get ; set ; }

		[ Fact ]
		[PushContext("test1")]
		public void Test1 ( )
		{
			AppLoggingConfigHelper.EnsureLoggingConfigured();
			
			//AppLoggingConfigHelper.EnsureLoggingConfigured();
			LB (  ).Level ( LogLevel.Warn ).Message("herro").Write();
		}
		private void DoLog ( string test )
		{
			LB ( ).Level ( LogLevel.Trace ).Message ( test ).Write ( ) ;
		}
		protected LogBuilder LB ( )
		{
			return new LogBuilder ( LogManager.GetCurrentClassLogger ( ) ).Property ( "stack" , MyStack);

		}

		[ Fact ]
		public void TestObjectIdExtension1 ( )
		{
			AppLoggingConfigHelper.EnsureLoggingConfigured();
			var s = "<appShared:ObjectId             xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"\r\n             xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"\r\n             xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" \r\n             xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008\"\r\n             xmlns:appShared=\"clr-namespace:AppShared;assembly=AppShared\"\r\n             xmlns:common=\"clr-namespace:Common\"\r\n/>" ;
			StringReader sr = new StringReader (
			                                    s
			                                   ) ;
			var load = XamlReader.Load ( new MemoryStream ( Encoding.UTF8.GetBytes ( s ) ) ) ;
			var x = load as ObjectIdExtension ;
			LB ( ).Level ( LogLevel.Info ).Message ( $"{x}" ) ;
		}
	}
}
