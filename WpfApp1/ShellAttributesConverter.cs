using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using NLog;
using Vanara.Windows.Shell;

namespace WpfApp1
{
    public class ShellAttributesConverter : IValueConverter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ShellItemAttribute att)
            {
                if (typeof(IEnumerable).IsAssignableFrom(targetType))
                {
                    var names = Enum.GetNames(typeof(ShellItemAttribute));
                    uint iatt = (uint) att;
                    List<ShellItemAttribute> r = new List<ShellItemAttribute>();
                    foreach(var val in Enum.GetValues(typeof(ShellItemAttribute)))
                    {
                        Logger.Debug(val.GetType().ToString);
                        uint ival = (uint) val;
                        if ((iatt & ival) == ival)
                        {
                            r.Add((ShellItemAttribute) val);
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
