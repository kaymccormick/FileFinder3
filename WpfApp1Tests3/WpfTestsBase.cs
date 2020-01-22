#region header

// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// WpfTestsBase.cs
// 
// 2020-01-22-7:06 AM
// 
// ---

#endregion

using System;
using System.Collections.Concurrent;
using System.Runtime.Serialization;
using Autofac;
using JetBrains.Annotations;
using NLog.Fluent;
using WpfApp1Tests3.Attributes;
using WpfApp1Tests3.Fixtures;
using WpfApp1Tests3.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WpfApp1Tests3
{
	public class WpfTestsBase
		: IClassFixture < ContainerFixture >,
			IDisposable,
			IHasId
	{
		protected ContainerFixture      _containerFixture;
		private   UtilsContainerFixture _utilsContainerFixture;
		private   MyServicesFixture     _myServicesFixture;

		[ThreadStatic]
		public static ConcurrentDictionary < object, long > Instances = new ConcurrentDictionary < object, long >();

		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public WpfTestsBase(
			WpfApplicationFixture fixture,
			ContainerFixture      containerFixture,
			ObjectIDFixture       objectIdFixture,
			UtilsContainerFixture utilsContainerFixture,
			ITestOutputHelper     outputHelper
		)
		{
#if LOG_HERE
			MyTarget = new XunitTarget( outputHelper );
			LogManager.Configuration.AddTarget( "xunit", MyTarget );
			//LogManager.Configuration.AddRule( LogLevel.Debug, LogLevel.Fatal, MyTarget, "*" );
			//LogManager.Configuration.LoggingRules.Add( new LoggingRule( "*", LogLevel.Debug, MyTarget ));
			LogManager.Configuration.LoggingRules.Insert( 0, new LoggingRule( "*", LogLevel.FromString( "Trace" ), MyTarget ) );
			LogManager.ReconfigExistingLoggers();

			WpfTests.Logger.Debug( "test" );
			MyTarget.Write( LogEventInfo.Create( LogLevel.Info, "test", "beep" ) );
#endif
			_myServicesFixture = utilsContainerFixture.Container.Resolve < MyServicesFixture >();
			//ContextStack<InfoContext>.DefaultAllowDuplicateNames = false; Instances.Add( this );
			Fixture                = fixture;
			ObjectIdFixture        = objectIdFixture;
			OutputHelper           = outputHelper;
			_containerFixture      = containerFixture;
			_utilsContainerFixture = utilsContainerFixture;
			MyStack                = InstanceFactory.CreateContextStack < InfoContext >();
			bool firstTime;
			ObjectId        = Generator.GetId( this, out firstTime );
			Instances[this] = ObjectId;

			Assert.True( firstTime );
		}

		public WpfApplicationFixture Fixture { get; }

		public ObjectIDFixture ObjectIdFixture { get; }

		public ITestOutputHelper OutputHelper { get; }

		[ ContextStackInstance ] public ContextStack < InfoContext > MyStack { get; }

		[ InfoContextFactory ]
		[ UsedImplicitly ]
		public InfoContext.Factory InfoContextFactory => myServices.InfoContextFactory;

		public ObjectIDFixture.GetObjectIdDelegate GetObjIdFunc => ObjectIdFixture.GetObjectId;

		public ObjectIDGenerator Generator => ObjectIdFixture.Generator;

		private IComponentContext UtilsContainer => _utilsContainerFixture.Container;

		protected IMyServices myServices => _myServicesFixture.MyServices;

		public Factory InstanceFactory => ObjectIdFixture.InstanceFactory;

		public long ObjectId { get; }

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing,
		///     or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			long myid;
			Instances.TryRemove( this, out myid );
#if LOG_HERE
			LogManager.Configuration.RemoveTarget( "xunit" );
			LogManager.Configuration.LoggingRules.RemoveAt( 0 );
			LogManager.ReconfigExistingLoggers();

#endif
		}

		public class MyServices : IMyServices
		{
			public MyServices(
				InfoContext.Factory infoContextFactory
			)
			{
				InfoContextFactory = infoContextFactory;
			}

			public InfoContext.Factory InfoContextFactory { get; }
		}

		protected IDisposable C(
			object             test,
			[CanBeNull] string name = null
		) => new AttachedContext(MyStack, InfoContextFactory(name, test));

		protected void DoLog(
			string test
		)
		{
			var logBuilder =
				Log.Warn().Message( test );
			logBuilder = logBuilder.Property( "stack", MyStack );
			//.Property( "context", MyStack.ToOrderedDictionary()) //.Property("stack", MyStack)

			logBuilder.Write();
		}
	}
}