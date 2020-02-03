using System ;
using System.Collections.Generic ;
using System.Collections.ObjectModel ;
using System.Linq ;
using AppShared.Interfaces ;
using Common.Logging ;
using NLog ;

namespace Common
{

    /// <summary></summary>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{T}" />
    /// <seealso cref="AppShared.Interfaces.IMenuItemList" />
    /// <seealso cref="AppShared.Interfaces.IHaveLogger" />
    /// <autogeneratedoc />
    /// TODO Edit XML Comment Template for MenuItemList
    [LoggingEntityMetadata (typeof(MenuItemList))]
    public class MenuItemList : ObservableCollection < IMenuItem >, IMenuItemList , IHaveLogger
    {
        /// <summary>Gets or sets the logger.</summary>
        /// <value>The logger.</value>
        /// <autogeneratedoc />
        /// TODO Edit XML Comment Template for Logger
        public ILogger Logger { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MenuItemList"/> class.</summary>
        /// <param name="topLevelMenus">The top level menus.</param>
        /// <param name="loggerFunc">The logger function.</param>
        public MenuItemList (
            IEnumerable < ITopLevelMenu > topLevelMenus,
            Func<Type, ILogger> loggerFunc
        ) : base( topLevelMenus.Select( menu => menu.GetXMenuItem() ) )
        {
            Logger = loggerFunc( typeof(MenuItemList) );
            Logger.Info( $"Creating {nameof( MenuItemList )} [ Count = {Count} ] " );
        }
    }
}