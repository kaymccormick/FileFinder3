using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace FileFinder3
{
    public class FileFinderImpl
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public Dictionary < string, ObjectInfo > infos;

//        public List<Regex> skipFiles = new List<Regex>({ new Regex("")});

        public string FindDir { get; set; }

        public DirectoryObjectInfo Root { get; set; }

        public IObserver < FileInfo > Observer { get; set; }

        public void FindFiles()
        {
            var root = new DirectoryObjectInfo();
            Root = root;
            Recurse( root, new DirectoryInfo( FindDir ), out var info );
        }

        private void Recurse(
            DirectoryObjectInfo     parent,
            DirectoryInfo           dir,
            out DirectoryObjectInfo result
        )
        {
            //Logger.Debug($"{dir.FullName}");
            IEnumerable < DirectoryInfo > dirs;

            var doi = new DirectoryObjectInfo( dir );
            parent.AddChild( doi );

            //Console.WriteLine(dir.FullName);
            try
            {
                dirs = dir.GetDirectories();
            }
            catch ( Exception e )
            {
                Logger.Warn(
                            $"Unable to retreive directories for {dir}: {e.Message}"
                           );
                result = null;
                return;
            }

            foreach ( var dir2 in dirs )
            {
                DirectoryObjectInfo subResult;
                Recurse( doi, dir2, out subResult );
                if ( subResult != null )
                {
                    doi.SummaryInfo.TotalSize +=
                        subResult.SummaryInfo.TotalSize;
                    doi.SummaryInfo.NumEntries +=
                        subResult.SummaryInfo.NumEntries;
                    foreach ( var q in subResult.Extensions )
                    {
	                    //SummaryInfo sext;
                        if ( ! doi.Extensions.TryGetValue( q.Key, out var info ) )
                        {
                            info                  = new ExtensionInfo();
                            doi.Extensions[q.Key] = info;
                        }

                        info.SummaryInfo.NumEntries +=
                            q.Value.SummaryInfo.NumEntries;
                        info.SummaryInfo.TotalSize +=
                            q.Value.SummaryInfo.TotalSize;
                        info.Items.AddRange( q.Value.Items );
                    }
                }
            }

            IList < FileInfo > files = dir.GetFiles();
            var s = doi.SummaryInfo;
            try
            {
                foreach ( var file in files )
                {
                    Observer.OnNext( file );
                    s.NumEntries++;
                    s.TotalSize += file.Length;

                    var extKey = file.Extension.ToLower();
                    if ( ! doi.Extensions.TryGetValue( extKey, out var ix ) )
                    {
                        ix                     = new ExtensionInfo();
                        doi.Extensions[extKey] = ix;
                    }

                    ix.SummaryInfo.NumEntries++;
                    ix.SummaryInfo.TotalSize += file.Length;

                    if ( string.Equals(
                                       file.Extension, ".mrimg",
                                       StringComparison.OrdinalIgnoreCase
                                      ) )
                    {
                    }
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

            result = doi;
        }
    }
}