using System ;
using System.Collections ;
using System.Collections.Generic ;
using System.Diagnostics ;
using System.Linq ;
using System.Windows ;
using AppShared ;
using AppShared.Interfaces ;
using Autofac ;
using Autofac.Core ;
using Autofac.Core.Activators.Reflection ;
using Common.Logging ;
using Common.Utils ;
using JetBrains.Annotations ;
using KayMcCormick.Dev.Test.Metadata ;
using NLog ;
using NLog.Config ;
using NLog.Layouts ;
using NLog.Targets ;
using WpfApp1.Windows ;
using WpfApp1Tests3.Attributes ;
using WpfApp1Tests3.Fixtures ;
using Xunit ;
using Xunit.Abstractions ;
using App = WpfApp1.Application.App ;
using LogLevel = NLog.LogLevel ;
using LogManager = NLog.LogManager ;

namespace WpfApp1Tests3
{
	[ WpfTestApplication ]
	[ Collection ( "WpfApp" ) ]
	[ BeforeAfterLogger ]
	public class RandomXaml // : IClassFixture <LoggingFixture>
	{
		private static readonly Logger Logger = LoggerProxyHelper.GetCurrentClassLogger ( ) ;

		public LoggerProxyHelper.LogMethod UseLogMethod { get ; set ; }

        [Fact]
public void Test1()
{
}

	}
}
