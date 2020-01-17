using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class MyFileInfo 
    {
        [XmlIgnore]
        public virtual FileSystemInfo FileSystemInfo { get; }

        public string Name
        {
            get => this.FileSystemInfo.Name;
        }

        //public Bitmap SmallThumbNailBitmap { get; set; }
        [XmlIgnore]
        public BitmapSource SmallThumbnailBitmapSource { get; set; }
        public bool IsLink { get; set; }
        public string ParsingName { get; set; }
        public SerializableDictionary<string, string> PropertyDict { get; set; } = new SerializableDictionary<string, string>();
    }

    public class MyFileFileInfo : MyFileInfo
    {
        [XmlIgnore]
        public override FileSystemInfo FileSystemInfo { get => FileInfo; }
        [XmlIgnore]
        public FileInfo FileInfo { get; set; }
    }
    public class MyDirectoryFileInfo : MyFileInfo
    {
        [XmlIgnore]
        public override FileSystemInfo FileSystemInfo { get => DirectoryInfo; }
        [XmlIgnore]
        public DirectoryInfo DirectoryInfo { get; set; }
    }
}