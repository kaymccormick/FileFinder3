using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SharpShell.Interop;
using Vanara.Windows.Shell;

namespace WpfApp1
{
    public class ShellFolderContentsConverter : IValueConverter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (value is ShellFolder shf)
            {
                Logger.Debug(targetType.ToString);
                if (typeof(IEnumerable).IsAssignableFrom(targetType))
                {

                    var enumerateChildren =
                        shf.EnumerateChildren(FolderItemFilter.Folders | FolderItemFilter.NonFolders);
                    var shellItems = enumerateChildren.ToList();
                    var r = new List<ItemWrapper>();
                    foreach (var item in shellItems)
                    {
                        ItemWrapper wrapper = new ItemWrapper(item);
                        r.Add(wrapper);
                    }

                    // Debug.Assert(shellItems.Any());
                    return r;
                    // return shellItems;
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