using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vanara.Windows.Shell;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ShellFun.xaml
    /// </summary>
    public partial class ShellFun : Window
    {
        public static readonly DependencyProperty CurrentShellFolderProperty = DependencyProperty.Register("CurrentShellFolder", typeof(ShellFolder), typeof(ShellFun),
            new FrameworkPropertyMetadata(ShellFolder.Desktop,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnCurrentShellFolderChanged),
                null, true,
                UpdateSourceTrigger.PropertyChanged));
        public static readonly RoutedEvent CurrentShellFolderChangedEvent = EventManager.RegisterRoutedEvent("CurrentShellFolderChanged",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<ShellFolder>),
            typeof(ShellFun));

        private static void OnCurrentShellFolderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedPropertyChangedEventArgs<ShellFolder> ev = new RoutedPropertyChangedEventArgs<ShellFolder>(
                (ShellFolder)e.OldValue, (ShellFolder)e.NewValue, CurrentShellFolderChangedEvent);
            ShellFun owner = (ShellFun)d;
            owner.RaiseEvent(ev);
        }

        public event RoutedEventHandler CurrentShellFolderChanged
        {
            add { AddHandler(CurrentShellFolderChangedEvent, value); }
            remove { RemoveHandler(CurrentShellFolderChangedEvent, value); }
        }

        public ShellFolder CurrentShellFolder
        {
            get => (ShellFolder) GetValue(CurrentShellFolderProperty);
            set => SetValue(CurrentShellFolderProperty, value);
        }

        public ShellFun()
        {
            InitializeComponent();
            
        }

        private void DesktopButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentShellFolder = ShellFolder.Desktop;
        }
    }
}
