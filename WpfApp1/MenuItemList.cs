using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NLog;

namespace WpfApp1
{
    public class MenuItemList : ObservableCollection < XMenuItem >
    {
        private static readonly Logger Logger =
            LogManager.GetCurrentClassLogger();

        public MenuItemList(
            IEnumerable < ITopLevelMenu > topLevelMenus
        ) : base( topLevelMenus.Select( menu => menu.GetXMenuItem() ) )
        {
            Logger.Info( $"{nameof( MenuItemList )}" );
        }
    }
}