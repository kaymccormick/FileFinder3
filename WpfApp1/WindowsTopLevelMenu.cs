    using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public class WindowsTopLevelMenu : ITopLevelMenu
    {
        private readonly Func<XMenuItem> _xMenuItemCreator;
        private readonly XMenuItem _xMenuItem;
        public IEnumerable<Lazy<Window>> Windows { get; }

        public WindowsTopLevelMenu(IEnumerable<Lazy<Window>> windows, Func<XMenuItem> xMenuItemCreator)
        {
            _xMenuItemCreator = xMenuItemCreator;
            _xMenuItem = xMenuItemCreator();
            Windows = windows;
        }

        public XMenuItem GetXMenuItem()
        {
            var root = _xMenuItem;
            _xMenuItem.Header = "Windows";
            _xMenuItem.Children = Windows.Select((lazy, i) =>
            {
                
                var m =
                    _xMenuItemCreator();
                m.Header = lazy.GetType().Name;
                m.Command = MyAppCommands.OpenWindow;
                m.CommandParameter = lazy;
                return m;
            }).ToList();
            return root;
        }
    }
}