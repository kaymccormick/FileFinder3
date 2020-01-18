using System.Windows;
using Microsoft.WindowsAPICodePack.Controls.WindowsPresentationFoundation;
using Microsoft.WindowsAPICodePack.Shell;

namespace WpfApp1
{
    /// <summary>
    ///     Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();

            var explorerBrowser = new ExplorerBrowser();
            DockPanel.Children.Add( explorerBrowser );
            explorerBrowser.NavigationTarget =
                ShellFileSystemFolder.FromFolderPath( @"C:\" );
        }
    }
}