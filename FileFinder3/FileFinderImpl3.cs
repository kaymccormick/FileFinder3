using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileFinder3
{
    public class FileFinderImpl3
    {
        public IObserver<FileSystemInfo> Observer;
        public IObservable<FileSystemInfo> MyObservable;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        
//        public List<Regex> skipFiles = new List<Regex>({ new Regex("")});

        public string FindDir { get; set; }

        public FileFinderImpl3()
        {
        }

        public void FindFiles()
        {
            Recurse(new DirectoryInfo(FindDir));
        }

        public DirectoryObjectInfo Root { get; set; }

        private void Recurse(DirectoryInfo dir)
        {
            Logger.Trace($"{dir.FullName}");
            IEnumerable<DirectoryInfo> dirs;
            Observer.OnNext(dir);
            
            try
            {
                dirs = dir.GetDirectories();
            }
            catch (Exception e)
            {
                Logger.Warn($"Unable to retreive directories for {dir}: {e.Message}");
                return;
            }

            foreach (var dir2 in dirs)
            {
                this.Recurse(dir2);
            }

            IList<FileInfo> files = dir.GetFiles();
            try
            {
                foreach (var file in files)
                {
                    Observer.OnNext(file);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Unable to completely process files in {dir.FullName}");
                throw;
            }
        }

    }
}
