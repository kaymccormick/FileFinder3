using System ;
using System.Reflection ;
using System.Threading ;
using System.Threading.Tasks ;
using System.Windows ;
using System.Windows.Markup ;
using System.Windows.Media ;
using System.Windows.Threading ;
using JetBrains.Annotations ;
using NLog ;
using Xunit ;

namespace TestLib.Fixtures
{
    public class WpfApplicationHelper : IAsyncLifetime
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ( ) ;

        public WpfApplicationHelper ( Assembly theAssembly ) { TheAssembly = theAssembly ; }

        public Assembly TheAssembly { get ; }

        public bool LoadRealApp { get ; set ; } = true ;

        public Uri BasePackUri { get ; set ; }

        public Assembly CurAssembly { get ; set ; }

        public Application MyApp { get ; set ; }

        public DispatcherOperation Op { get ; set ; }


        /// <summary>
        ///     Called immediately after the class has been created, before it is used.
        /// </summary>
        public Task InitializeAsync ( )
        {
            Logger?.Debug ( $"{nameof ( InitializeAsync )}" ) ;
            // return Task.Run (
                             // ( ) => {
                                 // CreateApplication ( TheAssembly ) ;
                             // }
                            // ) ;
                            return Task.CompletedTask ;
        }

        /// <summary>
        ///     Called when an object is no longer needed. Called just before
        ///     <see cref="M:System.IDisposable.Dispose" />
        ///     if the class also implements that.
        /// </summary>
        public Task DisposeAsync ( )
        {
            var tcs = new TaskCompletionSource < bool > ( ) ;
            var s = new CancellationTokenSource ( ) ;
            var token = s.Token ;
            if ( MyApp != null && MyApp.Dispatcher != null ) {
                var dispatcherOperation = MyApp.Dispatcher.InvokeAsync (
                                                                        ( ) => {
                                                                            Logger.Error (
                                                                                          "on dispatcher thread"
                                                                                         ) ;
                                                                            MyApp.Exit += (
                                                                                sender
                                                                              , args
                                                                            ) => {
                                                                                Logger.Error (
                                                                                              "exit handler"
                                                                                             ) ;
                                                                                Logger.Error (
                                                                                              "setting result to true"
                                                                                             ) ;
                                                                                tcs.SetResult ( true ) ;
                                                                            } ;
                                                                            Logger.Error (
                                                                                          "calling shutdown"
                                                                                         ) ;
                                                                            MyApp.Shutdown ( ) ;
                                                                            Logger.Error (
                                                                                          "shutdown returned"
                                                                                         ) ;
                                                                        }
                                                                      , DispatcherPriority.Send
                                                                      , token
                                                                       ) ;
                return Task.WhenAny (
                                     Task.WhenAll ( dispatcherOperation.Task , tcs.Task )
                                   , Task.Delay ( 3000 )
                                    ) ;
            }
            else
            {
                Logger.Error ( "MyApp is null" ) ;
            }

            return Task.CompletedTask ;
        }
//
//         private Task CreateApplication ( Assembly theAssembly )
//         {
//             Application theApp ;
//             App myApp = null ;
//             if ( LoadRealApp )
//             {
//                 Logger?.Debug ( "Creating real app" ) ;
//                 try
//                 {
//                     void doApp ( )
//                     {
//                         theApp              = myApp = new App ( ) ;
//                         MyApp               = myApp ;
//                         theApp.ShutdownMode = ShutdownMode.OnExplicitShutdown ;
//                         theApp.Run ( ) ;
//                     }
//
//                     myApp = new App ( ) ;
//                     myApp.DoOnStartup ( Array.Empty<string> ( ) ) ;
//                     return Task.CompletedTask ;
//                 }
//                 catch ( Exception ex )
//                 {
//                     Logger?.Error ( ex , ex.Message ) ;
//
//                     throw ex ;
//                 }
//
// #if XX
//                 Action < Application > runAction = application => application.Run ( ) ;
//                 var dispatcherOperation =
//                     myApp.Dispatcher.BeginInvoke ( DispatcherPriority.Send , runAction , myApp ) ;
//                 Op = dispatcherOperation ;
// #endif
//
//                 //myApp.AppInitialize();
//                 // var thread = new Thread (
//                 // o => {
//                 // ( ( Application ) o ).Run ( ) ;
//                 // }
//                 // ) ;
//                 // thread.Start(myApp);
//             }
//             else
//             {
//                 MyApp = new Application ( ) ;
//                 return Task.CompletedTask ;
//             }
//         }
//         //
//         //
//         // CurAssembly =
//         // 		theAssembly ?? throw new ArgumentNullException ( nameof ( theAssembly ) ) ;
//         //
//         // 	var assemblyFullName = CurAssembly.FullName ;
//         // 	var assPart = Uri.EscapeUriString ( CurAssembly.GetName ( ).Name ) ;
//         // 	var uri = new Uri (
//         // 	                   $"pack://application:,,,/{assPart};component/"
//         // 	                 , UriKind.RelativeOrAbsolute
//         // 	                  ) ;
//         // 	BasePackUri = uri ;
//         // 	Debug.WriteLine ( "basepackuri is " + BasePackUri ) ;
//         // 	MyApp = theApp ;
//         // 	return null ;
//         // }

        private Application LoadApplication ( )
        {
            // if ( LoadRealApp )
            // {
            //     Application app = new App ( ) ;
            //     return app ;
            // }

            return new Application ( ) ;
        }

        //[Test(), Apartment(ApartmentState.STA)]
        public void MakeWindowWrap ( [ NotNull ] Type genericType , [ NotNull ] Type wrappedType )
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
                var name = CurAssembly.GetName ( ).Name ;
                var wrapType = genericType.MakeGenericType ( wrappedType ) ;
                var wrap = Activator.CreateInstance ( wrapType ) as WindowWrap < Visual > ;
                MyApp.Run ( wrap ) ;
            }
            catch ( XamlParseException e )
            {
                Console.WriteLine ( e.Message ) ;
                throw ;
            }
            catch ( Exception e )
            {
                Logger.Debug ( e , $"{e.Message}" ) ;
                Console.WriteLine ( e ) ;
                throw ;
            }
        }
    }
}