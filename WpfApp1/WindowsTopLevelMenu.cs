using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public class WindowsTopLevelMenu : ITopLevelMenu
    {
        private readonly XMenuItem          _xMenuItem;
        private readonly Func < XMenuItem > _xMenuItemCreator;

        public WindowsTopLevelMenu(
            IEnumerable < Lazy < Window > > windows,
            Func < XMenuItem >              xMenuItemCreator
        )
        {
            _xMenuItemCreator = xMenuItemCreator;
            _xMenuItem        = xMenuItemCreator();
            Windows           = windows;
        }

        public IEnumerable < Lazy < Window > > Windows { get; }

        public XMenuItem GetXMenuItem()
        {
            var root = _xMenuItem;
            _xMenuItem.Header = "Windows";
            _xMenuItem.Children = Windows.Select(
                                                 (
                                                     lazy,
                                                     i
                                                 ) =>
                                                 {
                                                     var m =
                                                         _xMenuItemCreator();
                                                     m.Header =
                                                         lazy.GetType().Name;
                                                     m.Command =
                                                         MyAppCommands
                                                            .OpenWindow;
                                                     m.CommandParameter = lazy;
                                                     return m;
                                                 }
                                                ).ToList();
            return root;
        }
    }
}