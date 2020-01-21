using System.Collections;
using System.Collections.Generic;
using NLog;
using WpfApp1.Menus;

namespace WpfApp1.Interfaces
{
    public interface IMenuItemList: IList<IMenuItem>, ICollection<IMenuItem>, IEnumerable<IMenuItem>, IEnumerable, IList, ICollection, IReadOnlyList<IMenuItem>, IReadOnlyCollection<IMenuItem>
    {
    }
}