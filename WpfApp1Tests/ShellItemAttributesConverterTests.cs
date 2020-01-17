using NUnit.Framework;
using WpfApp1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanara.Windows.Shell;

namespace WpfApp1Tests
{
    [TestFixture()]
    public class ShellItemAttributesConverterTests
    {
        [Test()]
        public void ShellItemAttributesConverterTest()
        {
            ShellItemAttributesConverter converter = new ShellItemAttributesConverter();

        }

        [Test]
        public void ProcessAttributesTest()
        {
            ShellItemAttributesConverter converter = new ShellItemAttributesConverter();
            Assert.AreEqual(27, converter.ProcessAttributes.Count);
        }

        [Test]
        public void SummaryDictionaryTest()
        {
            ShellItemAttributesConverter converter = new ShellItemAttributesConverter();
            Assert.AreEqual(35, converter.SummaryDictionary.Count);
        }

        [Test]
        public void ConvertTest()
        {
            ShellItemAttributesConverter converter = new ShellItemAttributesConverter();
            ShellItemAttribute att = ShellItemAttribute.Browsable | ShellItemAttribute.CanCopy;
            var convert = converter.Convert(att, typeof(IEnumerable), null, CultureInfo.CurrentUICulture);
            Assert.IsInstanceOf<IEnumerable>(convert);
            IEnumerable x = (IEnumerable) convert;
            var enumerator = x.GetEnumerator();
            var item = enumerator.Current;
            enumerator.MoveNext();
            var item2 = enumerator.Current;

        }
    }
}