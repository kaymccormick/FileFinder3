using System.Collections.Generic;
using System.Windows.Input;
using WpfApp1.Menus;

namespace WpfApp1.Interfaces
{
    public interface IMenuItem : ICommandSource
    {
        string Header { get; set; }

        new object CommandParameter { get; set; }

        new ICommand Command {
            get;
            set;
        }

        IEnumerable < IMenuItem > Children { get; set; }
    }
}