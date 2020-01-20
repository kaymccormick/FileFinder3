using System;
using System.Linq;
using WpfApp1Tests3.Utils;
using Xunit;

namespace WpfApp1Tests3
{
    public class InfoContextTests
    {
        [ Fact ]
        public void EnumeratorTest()
        {
            var name = "test";
            var objectContext = "hello";
            InfoContext x = new InfoContext( name, objectContext );
            var objects = x.ToList();
            Assert.Equal(2, objects.Count);
            Assert.Equal( name, objects[0] );
            Assert.Equal(objectContext, objects[1]);
        }
    }
}
