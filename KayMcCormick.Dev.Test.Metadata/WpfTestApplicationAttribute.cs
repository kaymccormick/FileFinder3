using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayMcCormick.Dev.Test.Metadata
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Module)]
    public class WpfTestApplicationAttribute : Attribute
    {
    }
}
