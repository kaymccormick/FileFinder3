using System;
using FileFinder3;
using NUnit.Framework;

namespace FileFinder3Tests2
{
    public class FileFinderImplTests
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        [Test]
        public void FileFinder1Test()
        {
            FileFinderImpl finder = new FileFinderImpl();
            finder.FindDir = @"c:\temp";
            finder.FindFiles();

            var root = finder.Root;
            DumpInfo(root);
        }

        private static void DumpInfo(ObjectInfo root, int level = 0)
        {
            if (root is DirectoryObjectInfo d)
            {
                Logger.Debug($"{d.GetType().Name}");
                Logger.Debug($"{d.Path}");
                Logger.Debug($"NumEntries = {d.SummaryInfo.NumEntries}");
                Logger.Debug($"TotalSize = {d.SummaryInfo.TotalSize}");
                foreach(var ext in d.Extensions)
                {
                    Logger.Debug($"{ext.Key} = ${ext.Value}");
                }
                var rootChildren = d.Children;
                foreach (var child in rootChildren)
                {
                    Logger.Debug($"{new String(' ', level)}{child.Key}");
                    DumpInfo(child.Value, level + 1);
                }
            }
        }

        private static void DumpSummaryInfo(SummaryInfo extValue)
        {
            Logger.Debug(extValue.ToString);
        }
    }
}