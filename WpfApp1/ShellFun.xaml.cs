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
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public static readonly DependencyProperty CurrentShellFolderProperty = DependencyProperty.Register(
            "CurrentShellFolder", typeof(ShellFolder), typeof(ShellFun),
            new FrameworkPropertyMetadata(ShellFolder.Desktop,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnCurrentShellFolderChanged),
                null, true,
                UpdateSourceTrigger.PropertyChanged));

        public static readonly RoutedEvent CurrentShellFolderChangedEvent = EventManager.RegisterRoutedEvent(
            "CurrentShellFolderChanged",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<ShellFolder>),
            typeof(ShellFun));

        private static void OnCurrentShellFolderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedPropertyChangedEventArgs<ShellFolder> ev = new RoutedPropertyChangedEventArgs<ShellFolder>(
                (ShellFolder) e.OldValue, (ShellFolder) e.NewValue, CurrentShellFolderChangedEvent);
            ShellFun owner = (ShellFun) d;
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
            CommandManager.AddPreviewExecutedHandler(this, (sender, args) => { Logger.Debug(args.RoutedEvent.Name); });
            InputManager.Current.PostProcessInput += (sender, args) =>
            {
                DebugInput(args);
            };
            InputManager.Current.PreProcessInput += (sender, args) => { DebugInput(args); };
        }

        private static void DebugInput(ProcessInputEventArgs args)
        {
            return;
            if (args.StagingItem != null)
            {
                var i = args.StagingItem.Input;
                if (i.Device is MouseDevice)
                {
                    return;
                }
                Logger.Debug(args.GetType());

                if (i != null)
                {
                    if (i.RoutedEvent.Name == "PreviewInputReport")
                    {
                        Logger.Debug(i.RoutedEvent.OwnerType.ToString());
                        Logger.Debug(i.RoutedEvent.HandlerType.ToString());
                    }

                    // switch (i.RoutedEvent)
                    // {
                    //     Logger.Deb
                    //
                    // }
                    switch (i)
                    {
                        case KeyEventArgs k:
                            Logger.Debug($"{k.Key}");
                            break;
                        default:
                            Logger.Debug($"[{i.Device}] [{i.RoutedEvent.GetType()}] [{i.Source} {i.OriginalSource}]");
                            break;
                    }
                }
            }
        }

        private void DesktopButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentShellFolder = ShellFolder.Desktop;
        }

        private void AppSettingsOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            AppSettingsWindow appSettings = new AppSettingsWindow();
            appSettings.ShowDialog();
        }

        private void ContentsListView_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.None)
            {
                Logger.Debug("what to do");
            }
        }

        private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            RoutedUICommand c = e.Command as RoutedUICommand;
            CurrentShellFolder = (ShellFolder) ContentsListView.SelectedItem;
            //Logger.Debug("orig = " + e.OriginalSource);
        }
    }
}