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
		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public Class1 ( )
		{
			InstanceFactory = new Factory ( null ) ;
			MyStack         = InstanceFactory.CreateContextStack < InfoContext > ( ) ;
		}

		public ContextStack < InfoContext > MyStack { get ; }

		public Factory InstanceFactory { get ; }

		// ReSharper disable once UnusedMember.Local
		private void DoLog ( string test )
		{
			LB ( ).Level ( LogLevel.Trace ).Message ( test ).Write ( ) ;
		}

		// ReSharper disable once InconsistentNaming
		protected LogBuilder LB ( )
		{
			return new LogBuilder ( LogManager.GetCurrentClassLogger ( ) ).Property (
			                                                                         "stack"
			                                                                       , MyStack
			                                                                        ) ;
		}

		[ Fact ]
		[ PushContext ( "test1" ) ]
		public void Test1 ( )
		{
			AppLoggingConfigHelper.EnsureLoggingConfigured ( ) ;

			//AppLoggingConfigHelper.EnsureLoggingConfigured();
			LB ( ).Level ( LogLevel.Warn ).Message ( "Test log output message." ).Write ( ) ;
		}
	}
}