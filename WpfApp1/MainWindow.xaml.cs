using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileFinder3;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Binding x = new Binding()
            {

            };
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            CancellationTokenSource s = new CancellationTokenSource();
            Token = s.Token;
            Task.Run(() =>
            {
                FileFinderImpl f = new FileFinderImpl {FindDir = @"E:\"};
                f.FindFiles();
            }, Token);
        }

        public CancellationToken Token { get; set; }
    }
}
