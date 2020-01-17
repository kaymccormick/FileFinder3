using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
        public Dictionary<string, string> SummaryDictionary = new Dictionary<string, string>();
        public ShellItemAttributesConverter()
        {
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
                    foreach(var val in Enum.GetValues(typeof(ShellItemAttribute)))
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
