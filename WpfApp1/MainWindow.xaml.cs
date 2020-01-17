using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
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
using System.Windows.Threading;
using FileFinder3;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public InfoCollection Collection { get; set; }= new InfoCollection();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            Collection.CollectionChanged += CollectionOnCollectionChanged;
            var observable = Observable.Create<FileSystemInfo>(observer =>
            {
                Logger.Debug("herehere");
                FileFinderImpl f = new FileFinderImpl
                {
                    FindDir = @"e:\",
                    Observer = observer,
                };
                f.FindFiles();
                Logger.Info("done");
                return () => { };
            }).SubscribeOn(ThreadPoolScheduler.Instance).ObserveOnDispatcher(DispatcherPriority.ApplicationIdle);
            observable.Subscribe(info =>
            {
                Logger.Debug($"hi {info}");
                Collection.Add(info);
                //TreeView1.Items.Add(info);
            });
            Logger.Warn("Here");
        }

        private void CollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Logger.Debug("changed");
        }

        public CancellationToken Token { get; set; }
    }
}
