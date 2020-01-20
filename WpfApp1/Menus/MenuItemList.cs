using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NLog;
using WpfApp1.Interfaces;

namespace WpfApp1.Menus
{
    public class MenuItemList : ObservableCollection < IMenuItem >, IMenuItemList
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public MenuItemList(
            IEnumerable < ITopLevelMenu > topLevelMenus
        ) : base( topLevelMenus.Select( menu => menu.GetXMenuItem() ) )
        {
            Logger.Debug( $"{nameof( MenuItemList )} constructor" );
        }
    }
}