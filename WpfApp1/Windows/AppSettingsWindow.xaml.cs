using System.Windows ;
using WpfApp1.Attributes ;

namespace WpfApp1.Windows
{
    /// <summary>
    ///     Interaction logic for AppSettingsWindow.xaml
    /// </summary>
    ///
    [WindowMetadata("Application Settings")]
    public partial class AppSettingsWindow : Window
    {
	    public AppSettingsWindow()
        {
            InitializeComponent();
        }
    }
}
