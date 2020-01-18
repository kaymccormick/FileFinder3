using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileFinder3
{
    public class MountReflectImage
    {
        public string ReflectPath { get; set; } =
            @"C:\Program Files\Macrium\Reflect\Reflectbin.exe";

        public void MountImage(
            string path
        )
        {
            var driveInfos =
                DriveInfo.GetDrives().ToDictionary( info => info.Name );
            var x = new Regex( "([\"\\\\])", RegexOptions.Compiled );
            var p2 = x.Replace( path, "\\$1" );
            //Console.WriteLine(p2);

            var process = Process.Start( ReflectPath, $"{path} -bw -auto" );
            process.WaitForExit();
            var driveInfos2 = DriveInfo.GetDrives().Where(
                                                          (
                                                              info,
                                                              i
                                                          ) => ! driveInfos
                                                                  .ContainsKey(
                                                                               info
                                                                                  .Name
                                                                              )
                                                         );
            foreach ( var d in driveInfos2 )
            {
                Console.WriteLine( d );
            }

            var umount = Process.Start( ReflectPath, "-u" );
            umount.WaitForExit();
        }
    }
}