using System;
using System.Collections.Specialized;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using FileFinder3;
using Microsoft.WindowsAPICodePack.Shell;
using NLog;

namespace WpfApp1
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
        }

        public InfoCollection Collection { get; set; } = new InfoCollection();

        public CancellationToken Token { get; set; }

        private void StartButton_OnClick(
            object          sender,
            RoutedEventArgs e
        )
        {
            Collection.CollectionChanged += CollectionOnCollectionChanged;
            var observable = Observable.Create < FileSystemInfo >( observer => {
                Logger.Debug( "herehere" );
                var f = new FileFinderImpl3
                        {
                            FindDir  = @"e:\",
                            Observer = observer
                        };
                f.FindFiles();
                Logger.Info( "done" );
                return () => { };
            } ).SubscribeOn( ThreadPoolScheduler.Instance ).ObserveOnDispatcher( DispatcherPriority.ApplicationIdle );
            observable.Subscribe( info => {
                                     Logger.Debug( $"hi {info}" );
                                     MyFileInfo myInfo = null;
                                     switch ( info )
                                     {
                                         case FileInfo f:
                                             myInfo = new MyFileFileInfo { FileInfo = f };
                                             var f2 = ShellFile.FromFilePath( info.FullName );
                                             var bitmap = f2.Thumbnail.SmallBitmapSource;
                                             myInfo.SmallThumbnailBitmapSource = bitmap;
                                             break;
                                         case DirectoryInfo d:
                                             myInfo = new MyDirectoryFileInfo { DirectoryInfo = d };
                                             break;
                                     }

                                     if ( myInfo != null )
                                     {
                                         Collection.Add( myInfo );
                                     }

                                     //TreeView1.Items.Add(info);
                                 }
                                );
            Logger.Warn( "Here" );
        }

        private void CollectionOnCollectionChanged(
            object                           sender,
            NotifyCollectionChangedEventArgs e
        )
        {
            //Logger.Debug("changed");
        }
    }
}