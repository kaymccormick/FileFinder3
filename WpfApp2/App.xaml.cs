using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Application" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">More than one instance of the <see cref="T:System.Windows.Application" /> class is created per <see cref="T:System.AppDomain" />.</exception>
		public App()
		{
			var assembly = AppDomain.CurrentDomain.Load( @"..\..\WpfApp1\bin\debug\WpfApp1.exe" );
			var apps = assembly.GetExportedTypes().Where( (
				                                   type,
				                                   i
			                                   ) => type.IsSubclassOf( typeof(Application) ) );
			foreach(var app in apps)
			{
				Console.WriteLine(app);
			}
		}
	}
}
