using System.Windows ;
using WpfApp1.Attributes ;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Interaction logic for SampleWindow.xaml
    /// </summary>
    /// 
    [WindowMetadata("Sample Window")]
    public partial class SampleWindow : Window
    {
	    public SampleWindow()
        {
            InitializeComponent();
        }
    }
}
