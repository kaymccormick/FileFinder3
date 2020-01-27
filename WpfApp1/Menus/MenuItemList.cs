


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppShared.Interfaces ;
using Autofac.Features.AttributeFilters;
using NLog;

namespace WpfApp1.Menus
{
    [LoggingEntityMetadata(typeof(MenuItemList))]
    public class MenuItemList : ObservableCollection < IMenuItem >, IMenuItemList , IHaveLogger
    {
	    public ILogger Logger { get; set; }

	    public MenuItemList(
            IEnumerable < ITopLevelMenu > topLevelMenus,
            Func<Type, ILogger> loggerFunc
        ) : base( topLevelMenus.Select( menu => menu.GetXMenuItem() ) )
	    {
		    Logger = loggerFunc( typeof(MenuItemList) );
		    Logger.Info( $"Creating {nameof( MenuItemList )} [ Count = {Count} ] " );
	    }
    }
}
