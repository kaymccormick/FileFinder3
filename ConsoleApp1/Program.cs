using System;
using System.IO;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using FileFinder3;
using NLog;

namespace ConsoleApp1
{
    internal class Program
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        private static async Task Main()
        {
            var subject = new ReplaySubject < FileSystemInfo >();
            subject.Subscribe( info => {Logger.Debug( $"got {info}" );} );
            var task = Task.Run(
                                () => {
                                    var finder = new FileFinderImpl3
                                                 {
                                                     FindDir
                                                         = @"c:\temp",
                                                     Observer = subject
                                                 };
                                    finder.FindFiles();
                                }
                               );
            await task;
        }
    }
}