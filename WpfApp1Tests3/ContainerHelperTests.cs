using Xunit;
using WpfApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Util;

namespace WpfApp1Tests3
{
    public class ContainerHelperTests
    {
        [Fact()]
        public void SetupContainerTest()
        {
            var c = ContainerHelper.SetupContainer();
        }
    }
}