using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace FileFinder3
{
    public class FileFinderImpl3
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public IObservable < FileSystemInfo > MyObservable;
        public IObserver < FileSystemInfo >   Observer;

        //        public List<Regex> skipFiles = new List<Regex>({ new Regex("")});

        public string FindDir { get; set; }

        public DirectoryObjectInfo Root { get; set; }

        public void FindFiles()
        {
            Recurse( new DirectoryInfo( FindDir ) );
        }

        private void Recurse(
            DirectoryInfo dir
        )
        {
            Logger.Trace( $"{dir.FullName}" );
            IEnumerable < DirectoryInfo > dirs;

            Observer.OnNext( dir );

            try
            {
                dirs = dir.GetDirectories();
            }
            catch ( Exception e )
            {
                Logger.Warn(
                            $"Unable to retreive directories for {dir}: {e.Message}"
                           );
                return;
            }

            foreach ( var dir2 in dirs )
            {
                Recurse( dir2 );
            }

            IList < FileInfo > files = dir.GetFiles();
            try
            {
                foreach ( var file in files )
                {
                    Observer.OnNext( file );
                }
            }
            catch ( Exception e )
            {
                Logger.Error(
                             e,
                             $"Unable to completely process files in {dir.FullName}"
                            );
                throw;
            }
        }
    }
}