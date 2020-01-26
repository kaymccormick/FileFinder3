using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using JetBrains.Annotations ;
using WpfApp1.Application ;
using Xunit;

namespace WpfApp1Tests3.WpfUtils
{
	public class WpfApplicationHelper : IAsyncLifetime
	{
		private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
		private                 Uri         _basePackUri;

		public WpfApplicationHelper(
			Assembly theAssembly
		)
		{
			Application ret ;
			if(LoadRealApp)
			{
				var application = new App 
					( ) ;
				
				Application app1 = application ;
					ret = app1 ;
			}
			else
			{
				ret = new Application() ;
			}

			var app = ret ;
			
			CurAssembly = theAssembly ?? throw new ArgumentNullException ( nameof ( theAssembly ) );

			string assemblyFullName = CurAssembly.FullName;
			var assPart = Uri.EscapeUriString( CurAssembly.GetName().Name );
			var uri = new Uri(
			                  $"pack://application:,,,/{assPart};component/",
			                  UriKind.RelativeOrAbsolute );
			BasePackUri = uri;
			
			MyApp = app;
		}

		private Application LoadApplication ( )
		{
			
			if(LoadRealApp)
			{
				Application app = new App 
( ) ;
				return app ;
			}
			else
			{
				return new Application();
			}
		}

		public bool LoadRealApp { get ; set ; } = true ;

		public Uri BasePackUri
		{
			get { return _basePackUri; }
			set { _basePackUri = value; }
		}

		public Assembly CurAssembly { get; set; }

		public Application MyApp { get; set; }

		//[Test(), Apartment(ApartmentState.STA)]
		public void MakeWindowWrap(
			[ NotNull ] Type genericType, [ NotNull ] Type wrappedType
		)
		{
			if ( genericType == null )
			{
				throw new ArgumentNullException ( nameof ( genericType ) ) ;
			}

			if ( wrappedType == null )
			{
				throw new ArgumentNullException ( nameof ( wrappedType ) ) ;
			}

			try
			{
				var name = CurAssembly.GetName().Name;
				var wrapType = genericType.MakeGenericType( wrappedType );
				WindowWrap < Visual > wrap = Activator.CreateInstance( wrapType ) as WindowWrap < Visual >;
				MyApp.Run( wrap );
			}
			catch ( XamlParseException e )
			{
				Console.WriteLine( e.Message );
				throw;
			}
			catch ( Exception e )
			{
				Logger.Debug( e, $"{e.Message}" );
				Console.WriteLine( e );
				throw;
			}
		}



		/// <summary>
		/// Called immediately after the class has been created, before it is used.
		/// </summary>
		public Task InitializeAsync()
		{
			Logger.Debug($"{nameof(InitializeAsync)}"  ); 
			return Task.FromResult<object>(null);
		}

		/// <summary>
		/// Called when an object is no longer needed. Called just before <see cref="M:System.IDisposable.Dispose" />
		/// if the class also implements that.
		/// </summary>
		public Task DisposeAsync()
		{
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
			CancellationTokenSource s = new CancellationTokenSource(
			                                                       );
			var token = s.Token;
			var dispatcherOperation = MyApp.Dispatcher.InvokeAsync(() => {
				Logger.Error("on dispatcher thread");
				MyApp.Exit += (
					sender,
					args
				) => {
					Logger.Error("exit handler");
					Logger.Error("setting result to true");
					tcs.SetResult(true);
				};
				Logger.Error("calling shutdown");
				MyApp.Shutdown();
				Logger.Error("shutdown returned");
			}, DispatcherPriority.Send, token);
			return Task.WhenAny( Task.WhenAll( dispatcherOperation.Task, tcs.Task ),
			                     Task.Delay( 3000 ) );	

		}
	}
}