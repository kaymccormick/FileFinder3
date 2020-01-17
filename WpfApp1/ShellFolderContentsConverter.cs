using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Vanara.Windows.Shell;

namespace WpfApp1
{
    public class ShellFolderContentsConverter : IValueConverter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ShellFolder shf)
            {
                Logger.Debug(targetType.ToString);
                if (typeof(IEnumerable).IsAssignableFrom(targetType))
                {
                    {
                        return shf.EnumerateChildren(FolderItemFilter.FlatList);
                    }
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