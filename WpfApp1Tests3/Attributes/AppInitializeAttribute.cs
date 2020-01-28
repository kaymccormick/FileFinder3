#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// AppInitializeAttribute.cs
// 
// 2020-01-26-7:46 AM
// 
// ---
#endregion
using System ;
using System.Reflection ;
using System.Threading.Tasks ;
using System.Windows ;
using NLog ;
using WpfApp1.Application ;
using Xunit.Sdk ;

namespace WpfApp1Tests3.Attributes
{
	public class AppInitializeAttribute
		: BeforeAfterTestAttribute
	{
		private static readonly Logger Logger =
			LogManager.GetCurrentClassLogger();


		/// <summary>
		/// This method is called after the test method is executed.
		/// </summary>
		/// <param name="methodUnderTest">The method under test</param>
		public override void After ( MethodInfo methodUnderTest ) { base.After ( methodUnderTest ) ; }

		/// <summary>
		/// This method is called before the test metphod is executed.
		/// </summary>
		/// <param name="methodUnderTest">The method under test</param>
		public override void Before ( MethodInfo methodUnderTest )
		{
			// Logger.Trace ( $"Nefore" ) ;
			// Application.LoadComponent (
			                           // Application.Current
			                         // , new Uri ( "Applications/App.xaml" )
	                                
			                          // ) ;

			MyApp.TCS.Task.Wait ( ) ;
			base.Before ( methodUnderTest ) ;
		}

		public App MyApp {get ; set ; }
	
	}
}
