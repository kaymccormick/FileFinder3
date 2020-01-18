using FileFinder3;
using NUnit.Framework;

namespace FileFinder3Tests2
{
    [ TestFixture ]
    public class MountReflectImageTests
    {
        [ Test ]
        public void MountImageTest()
        {
            // FileFinderImpl finder = new FileFinderImpl();
            // finder.FindDir = @"E:\";
            //        finder.FindFiles();
            return;
            var x = new MountReflectImage();
            var path =
                "F:\\Reflect\\livingroom\\dyndisk2_samsunghd204UI_1aq1\\8E59629E42FB0283-00-00.mrimg";
            x.MountImage(
                         path
                        ); //@"E:\Reflect\PEGASOS-c\8D41DF2C5EA505B3-03-03.mrimg");

            Assert.Fail();
        }
    }
}