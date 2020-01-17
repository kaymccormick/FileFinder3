using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileFinder3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFinder3Tests
{
    [TestClass()]
    public class MountReflectImageTests
    {
        [TestMethod()]
        public void MountImageTest()
        {
            MountReflectImage x = new MountReflectImage();
            var path = "F:\\Reflect\\livingroom\\dyndisk2_samsunghd204UI_1aq1\\8E59629E42FB0283-00-00.mrimg";
            x.MountImage(path);//@"E:\Reflect\PEGASOS-c\8D41DF2C5EA505B3-03-03.mrimg");
        }
        
    }
}