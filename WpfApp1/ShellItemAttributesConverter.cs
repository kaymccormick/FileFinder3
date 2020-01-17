using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;
using NLog;
using Vanara.Windows.Shell;

namespace WpfApp1
{
    public class ShellItemAttributesConverter : IValueConverter
    {
        public Type Type = typeof(ShellItemAttribute);
        public HashSet<ShellItemAttribute> ProcessAttributes = new HashSet<ShellItemAttribute>();
        public Dictionary<string, string> SummaryDictionary = new Dictionary<string, string>();
        public ShellItemAttributesConverter()
        {
            var fieldInfos = Type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.ExactBinding);
            foreach (var f in fieldInfos)
            {
                Logger.Debug(f.Name);
                var value = f.GetValue(null);
                uint v = (uint) value;
                if (NumOfSetBits(v) == 1)
                {
                    ProcessAttributes.Add((ShellItemAttribute) value);
                }

                ProcessAttributes.Remove(ShellItemAttribute.ContentsMask);
            }
            //Logger.Debug($"{String.Join(", ", fieldInfos)}");
            var xml = Path.ChangeExtension(typeof(ShellItemAttribute).Assembly.Location, ".xml");
            if (File.Exists(xml))
            {
                var docuDoc = new XmlDocument();
                docuDoc.Load(xml);
                string path = "T:" + typeof(ShellItemAttribute).FullName;

                XmlNode xmlDocu = docuDoc.SelectSingleNode(
                    "//member[starts-with(@name, '" + path + "')]/summary");
                Logger.Debug(xmlDocu.InnerText);

                foreach (var name in Enum.GetNames(typeof(ShellItemAttribute)))
                {
                    path = "F:" + typeof(ShellItemAttribute).FullName + '.' + name;
                    var xPathExpr= "//member[starts-with(@name, '" + path + "')]/summary";
                    Logger.Trace(xPathExpr);
                    XmlNode xmlDocu2 = docuDoc.SelectSingleNode(xPathExpr);
                    SummaryDictionary[name] = xmlDocu2 != null ? xmlDocu2.InnerText : "";
                }

            }
        }

        public static uint NumOfSetBits(uint n)
        {
            uint count = 0;

            for (; n != 0; n >>= 1)
                count += (n & 1);   // check last bit

            return count;
        }

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ShellItemAttribute att)
            {
                if (typeof(IEnumerable).IsAssignableFrom(targetType))
                {
                    var names = Enum.GetNames(typeof(ShellItemAttribute));
                    uint iatt = (uint) att;
                    List<ShellItemAttributeListItem> r = new List<ShellItemAttributeListItem>();
                    var values = ProcessAttributes;
                    Logger.Debug($"{String.Join(" ", values)}");
                    foreach(var val in values)
                    {
                        Logger.Debug($"{val}");
                        uint ival = (uint) val;
			            Logger.Debug($"{iatt:X} & {ival:X} = {iatt & ival:X}");
                        if ((iatt & ival) == ival)
                        {
                            var valStr = val.ToString();
                            Logger.Debug(valStr);
                            string summary = null;
                            SummaryDictionary.TryGetValue(valStr, out summary);
                            r.Add(new ShellItemAttributeListItem((ShellItemAttribute) val, summary));
                        }
                    }

                    return r;
                }
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
