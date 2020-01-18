using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public class WindowsTopLevelMenu : ITopLevelMenu
    {
        public IEnumerable<Lazy<Window>> Windows { get; }

        public WindowsTopLevelMenu(IEnumerable<Lazy<Window>> windows)
        {
            Windows = windows;
        }

        public XMenuItem GetXMenuItem()
        {
            var root = new XMenuItem()
            {
                Header = "Windows",
                Children = Windows.Select((lazy, i) => new XMenuItem()
                {
                    Header = lazy.GetType().Name,
                    Command = MyAppCommands.OpenWindow,
                    CommandParameter = lazy,
                }).ToList(),
            };
            return root;
        }
    }
}