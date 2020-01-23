using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Autofac.Features.AttributeFilters;
using NLog;
using WpfApp1.Interfaces;

namespace WpfApp1.Menus
{
    [LoggingEntityMetadata(typeof(MenuItemList))]
    public class MenuItemList : ObservableCollection < IMenuItem >, IMenuItemList, ILoggingEntity
    {
	    public ILogger Logger { get; }

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
