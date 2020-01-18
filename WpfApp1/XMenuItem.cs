using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public class XMenuItem : ICommandSource
    {
        public string Header { get; set; }
        public IEnumerable<XMenuItem> Children { get; set; }
        public ICommand Command { get; set; }
        public object CommandParameter { get; set;  }
        public IInputElement CommandTarget { get; set;  }
    }
}