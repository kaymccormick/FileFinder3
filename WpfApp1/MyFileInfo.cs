using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public class MyFileInfo 
    {
        public virtual FileSystemInfo FileSystemInfo { get; }

        public string Name
        {
            get => this.FileSystemInfo.Name;
        }

        //public Bitmap SmallThumbNailBitmap { get; set; }
        public BitmapSource SmallThumbNailBitmapSource { get; set; }
    }

    public class MyFileFileInfo : MyFileInfo
    {
        public override FileSystemInfo FileSystemInfo { get => FileInfo; }
        public FileInfo FileInfo { get; set; }
    }
    public class MyDirectoryFileInfo : MyFileInfo
    {
        public override FileSystemInfo FileSystemInfo { get => DirectoryInfo; }
        public DirectoryInfo DirectoryInfo { get; set; }
    }
}