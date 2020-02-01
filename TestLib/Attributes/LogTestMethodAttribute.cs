using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection ;
using System.Text;
using System.Threading.Tasks;
using NLog ;
using Xunit.Sdk ;

namespace TestLib.Attributes
{
    public class LogTestMethodAttribute : BeforeAfterTestAttribute
    {
        /// <summary>
        /// This method is called after the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public override void After ( MethodInfo methodUnderTest )
        {
            LogManager.GetLogger(methodUnderTest.DeclaringType.ToString()).Info($"{nameof(After)} test method {methodUnderTest.Name}" ) ;
        }

        /// <summary>
        /// This method is called before the test method is executed.
        /// </summary>
        /// <param name="methodUnderTest">The method under test</param>
        public override void Before ( MethodInfo methodUnderTest )
        {
            LogManager.GetLogger(methodUnderTest.DeclaringType.ToString()).Info($"{nameof(Before)} test method {methodUnderTest.Name}" ) ;
        }
    }
}
