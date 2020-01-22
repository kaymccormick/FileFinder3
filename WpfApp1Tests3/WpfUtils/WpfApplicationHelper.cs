using System;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace WpfApp1Tests3.WpfUtils
{
    public class WpfApplicationHelper : IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private Uri _basePackUri;

        public WpfApplicationHelper(
            Assembly theAssembly
        )
        {
            Application app = new Application();
            CurAssembly = theAssembly;

            string assemblyFullName = CurAssembly.FullName;
            var assPart = Uri.EscapeUriString( CurAssembly.GetName().Name );
            var uri = new Uri(
                              $"pack://application:,,,/{assPart};component/",
                              UriKind.RelativeOrAbsolute );
            BasePackUri = uri;
            // var stream = Application.GetResourceStream(new Uri(
            //     $"pack://application:,,,/{assPart};component/dictionary.xaml",
            //     UriKind.RelativeOrAbsolute));
            try
            {
                System.Uri resourceLocater =
                    new System.Uri( "/ParseLogsControls;component/dictionary.xaml", System.UriKind.Relative );
                app.Resources = (ResourceDictionary)Application.LoadComponent( resourceLocater );
            }
            catch ( Exception )
            {
            }

            MyApp = app;
        }

        public Uri BasePackUri
        {
            get { return _basePackUri; }
            set { _basePackUri = value; }
        }

        public Assembly CurAssembly { get; set; }

        public Application MyApp { get; set; }

        private static void DisplayTypeInfo(
            Type t
        )
        {
            Console.WriteLine( "\r\n{0}", t );

            Console.WriteLine( "\tIs this a generic type definition? {0}",
                               t.IsGenericTypeDefinition );

            Console.WriteLine( "\tIs it a generic type? {0}",
                               t.IsGenericType );

            Type[] typeArguments = t.GetGenericArguments();
            Console.WriteLine( "\tList type arguments ({0}):", typeArguments.Length );
            foreach ( Type tParam in typeArguments )
            {
                Console.WriteLine( "\t\t{0}", tParam );
            }
        }

        //[Test(), Apartment(ApartmentState.STA)]
        public void MakeWindowWrap(
            Type genericType,
            Type wrappedType
        )
        {
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


        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            MyApp.Dispatcher.InvokeShutdown();
            //MyApp?.Shutdown();
        }
    }
}