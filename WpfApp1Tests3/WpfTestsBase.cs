﻿#region header
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

using System ;
using System.Collections.Concurrent ;
using System.Runtime.Serialization ;
using System.Threading ;
using System.Threading.Tasks ;
using AppShared ;
using AppShared.Interfaces ;
using Autofac ;
using Common.Context ;
using JetBrains.Annotations ;
using NLog ;
using NLog.Fluent ;
using TestLib.Fixtures ;
using WpfApp1.Attributes ;
using WpfApp1.Util ;
using WpfApp1Tests3.Attributes ;
using WpfApp1Tests3.Fixtures ;
using Xunit ;
using Xunit.Abstractions ;

namespace WpfApp1Tests3
{
	public class WpfTestsBase : IClassFixture < ContainerFixture > , IAsyncLifetime , IHasId
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger ( ) ;

		[ ThreadStatic ] internal static ConcurrentDictionary < object , long > Instances  ;
		// ReSharper disable once MemberCanBePrivate.Global
		protected readonly               ContainerFixture                       _containerFixture ;


		private readonly MyServicesFixture     _myServicesFixture ;
		private readonly UtilsContainerFixture _utilsContainerFixture ;

		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" />
		///     class.
		/// </summary>
		public WpfTestsBase (
			WpfApplicationFixture fixture
		  , ContainerFixture      containerFixture
		  , ObjectIDFixture       objectIdFixture
		  , UtilsContainerFixture utilsContainerFixture
		  , ITestOutputHelper     outputHelper
		)
		{
			_myServicesFixture = utilsContainerFixture.Container.Resolve < MyServicesFixture > ( ) ;
			Fixture =
				fixture ?? throw new ArgumentNullException ( nameof ( fixture ) ) ;
			ObjectIdFixture = objectIdFixture
			                  ?? throw new ArgumentNullException (
			                                                      nameof ( objectIdFixture )
			                                                     ) ;
			OutputHelper = outputHelper
			               ?? throw new ArgumentNullException (
			                                                   nameof ( outputHelper )
			                                                  ) ;
			_containerFixture = containerFixture
			                    ?? throw new ArgumentNullException (
			                                                        nameof ( containerFixture )
			                                                       ) ;
			containerScope = _containerFixture.LifetimeScope.BeginLifetimeScope ( ) ;
			_utilsContainerFixture = utilsContainerFixture
			                         ?? throw new ArgumentNullException (
			                                                             nameof (
				                                                             utilsContainerFixture )
			                                                            ) ;
			MyStack = InstanceFactory.CreateContextStack < InfoContext > ( ) ;
			bool firstTime ;
			ObjectId = Generator.GetId ( this , out firstTime ) ;
			if ( Instances == null )
			{
				Logger.Trace ( "Creating instances" ) ;
				Instances = new ConcurrentDictionary < object , long > ( ) ;
			}
			else
			{
				Logger.Trace ( "Not Creating instances" ) ;
			}

			Logger.Trace ( $"Setting Instances[this] to {ObjectId:0,8x}" ) ;
			Instances[ this ] = ObjectId ;
			var x = Thread.CurrentThread ;
			if ( x.Name == null )
			{
				x.Name = $"Testing thread {GetType ( )}[{ObjectId:0,8x}]" ;
			}

			Assert.True ( firstTime ) ;
		}

		public ILifetimeScope containerScope { get ; set ; }

		public WpfApplicationFixture Fixture { get ; }

		public ObjectIDFixture ObjectIdFixture { get ; }

		public ITestOutputHelper OutputHelper { get ; }

		[ ContextStackInstance ] public ContextStack < InfoContext > MyStack { get ; }

		[ InfoContextFactory ]
		[ UsedImplicitly ]
		public InfoContext.Factory InfoContextFactory => myServices.InfoContextFactory ;

		public ObjectIDFixture.GetObjectIdDelegate GetObjIdFunc => ObjectIdFixture.GetObjectId ;

		public ObjectIDGenerator Generator => ObjectIdFixture.Generator ;

		private IComponentContext UtilsContainer => _utilsContainerFixture.Container ;

		protected IMyServices myServices => _myServicesFixture.MyServices ;

		public Factory InstanceFactory => ObjectIdFixture.InstanceFactory ;

		/// <summary>
		///     Called immediately after the class has been created, before it is used.
		/// </summary>
		public Task InitializeAsync ( )
		{
			Logger.Trace ( $"{nameof ( InitializeAsync )}" ) ;
			return Task.CompletedTask ;
		}

		/// <summary>
		///     Called when an object is no longer needed. Called just before
		///     <see cref="M:System.IDisposable.Dispose" />
		///     if the class also implements that.
		/// </summary>
		public Task DisposeAsync ( )
		{
			long myid ;
			Instances.TryRemove ( this , out myid ) ;
			containerScope.Dispose ( ) ;
			return Task.CompletedTask ;
		}

		public long ObjectId { get ; }

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing,
		///     or resetting unmanaged resources.
		/// </summary>
		public void Dispose ( ) { }

		protected IDisposable C ( object test , [ CanBeNull ] string name = null )
		{
			return new AttachedContext ( MyStack , InfoContextFactory ( name , test ) ) ;
		}

		protected LogBuilder LB ( )
		{
			return new LogBuilder ( LogManager.GetCurrentClassLogger ( ) ).Property (
			                                                                         "stack"
			                                                                       , MyStack
			                                                                        ) ;
		}

		public class MyServices : WpfApp1Tests3.IMyServices
		{
			public MyServices ( InfoContext.Factory infoContextFactory )
			{
				InfoContextFactory = infoContextFactory ;
			}

			public InfoContext.Factory InfoContextFactory { get ; }
		}
	}
}