using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using FileFinder3;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static async Task Main()
        {
            ReplaySubject<FileSystemInfo> subject = new ReplaySubject<FileSystemInfo>();
            subject.Subscribe(info => { Logger.Debug($"got {info}"); });
            var task = Task.Run(() =>
            {
                FileFinderImpl3 finder = new FileFinderImpl3()
                {
                    FindDir = @"c:\temp",
                    Observer = subject,
                };
                finder.FindFiles();
            });
            await task;

        }
    }
}
