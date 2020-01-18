using System.IO;

namespace FileFinder3
{
    public class ObjectInfo
    {
        public ObjectInfo()
        {
        }

        public ObjectInfo(
            FileSystemInfo fsinfo
        )
        {
            Path      = fsinfo.FullName;
            Component = fsinfo.Name;
        }

        public ObjectInfo(
            string path
        )
        {
            Path = path;
        }

        public string Path { get; set; }

        public string Component { get; set; }
    }
}