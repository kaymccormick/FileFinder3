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
using System.Xml.Serialization;
using FileFinder3;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public InfoCollection Collection { get; set; } = new InfoCollection();

        public MainWindow2()
        {
            InitializeComponent();
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            Collection.CollectionChanged += CollectionOnCollectionChanged;
            var observable = Observable.Create<FileSystemInfo>(observer =>
            {
                Logger.Debug("herehere");
                var f = new FileFinderImpl3
                {
                    FindDir = @"c:\temp",
                    Observer = observer,
                };
                f.FindFiles();
                Logger.Info("done");
                return () => { };
            }).SubscribeOn(ThreadPoolScheduler.Instance).ObserveOnDispatcher(DispatcherPriority.ApplicationIdle);
            observable.Subscribe(info =>
            {
                Logger.Debug($"hi {info}");
                MyFileInfo myInfo = null;

                ShellObject sho = null;
                switch (info)
                {
                    case FileInfo f:
                        myInfo = new MyFileFileInfo() {FileInfo = f};
                        ShellFile f2 = ShellFile.FromFilePath(info.FullName);
                        sho = f2;
                        var bitmap = f2.Thumbnail.SmallBitmapSource;
                        myInfo.SmallThumbnailBitmapSource = bitmap;
                        myInfo.IsLink = f2.IsLink;
                        myInfo.ParsingName = f2.ParsingName;
                        XmlSerializer x = new XmlSerializer(myInfo.GetType());
                        var xx = new StringWriter();
                        x.Serialize(xx, myInfo);
                        Console.WriteLine(xx.ToString());
                        break;
                    case DirectoryInfo d:
                        myInfo = new MyDirectoryFileInfo() {DirectoryInfo = d};
                        ShellFileSystemFolder folder = ShellFileSystemFolder.FromFolderPath(d.FullName);
                        sho = folder;
                        myInfo.SmallThumbnailBitmapSource = folder.Thumbnail.SmallBitmapSource;
                        // ShellFile f22 = ShellFile.FromFilePath(info.FullName);
                        // var bitmap2 = f22.Thumbnail.SmallBitmapSource;
                        // myInfo.SmallThumbnailBitmapSource = bitmap2;
                        break;
                }

                if (sho != null)
                {
                    var props = sho.Properties.DefaultPropertyCollection;
                    foreach (var q in props)
                    {
                        var propdesc = q.Description;
                        var d = propdesc.DisplayName;
                        var desc = d ?? q.CanonicalName;
                        var formatForDisplay = q.FormatForDisplay(PropertyDescriptionFormatOptions.None);

                        Logger.Debug($"{info.Name} {desc} {formatForDisplay}");
                    }

#if DOPROP
                        myInfo.PropertyDict = new SerializableDictionary<string, string>(props
                            .Where(property => property.CanonicalName != null).ToDictionary(
                                property => property.Description.DisplayName,
                                property =>
                                {
                                    try
                                    {
                                        return property.FormatForDisplay(PropertyDescriptionFormatOptions.None);
                                    }
                                    catch (Exception ex)
                                    {
                                        return ex.Message;
                                    }
                                }));
#endif
                }

                if (myInfo != null)
                {
                    Collection.Add(myInfo);
                }

                //TreeView1.Items.Add(info);
            });
            Logger.Warn("Here");
        }

        private void CollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Logger.Debug("changed");
        }

        public CancellationToken Token { get; set; }

        private void MainWindow2_OnLoaded(object sender, RoutedEventArgs e)
        {
            TestWindow test = new TestWindow
                              {
                                  VerticalAlignment = VerticalAlignment.Stretch,
                                  HorizontalAlignment =
                                      HorizontalAlignment.Stretch
                              };
            test.Show();
        }
    }
}