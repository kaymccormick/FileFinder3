using System.IO;

namespace FileFinder3
{
    public class ObjectInfo
    {
        public string Path { get; set; }

        public ObjectInfo()
        {
        }

        public ObjectInfo(FileSystemInfo fsinfo)
        {
            Path = fsinfo.FullName;
            Component = fsinfo.Name;
        }

        public string Component { get; set; }

        public ObjectInfo(string path)
        {
            Path = path;
        }
    }
}