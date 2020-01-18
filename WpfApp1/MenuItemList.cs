using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.Scripting.Utils;

namespace WpfApp1
{
    public class MenuItemList : ObservableCollection<XMenuItem>
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public MenuItemList(IEnumerable<ITopLevelMenu> topLevelMenus) : base(topLevelMenus.Select(menu => menu.GetXMenuItem()))
        {
            Logger.Info($"{nameof(MenuItemList)}");
        }
    }
}