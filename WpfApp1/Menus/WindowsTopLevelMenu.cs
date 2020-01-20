using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Interfaces;

namespace WpfApp1.Menus
{
    public class WindowsTopLevelMenu : ITopLevelMenu
    {
        private readonly IMenuItem          _xMenuItem;
        private readonly Func < IMenuItem > _xMenuItemCreator;

        public WindowsTopLevelMenu(
            IEnumerable < Lazy < Window > > windows,
            Func < IMenuItem >              xMenuItemCreator
        )
        {
            _xMenuItemCreator = xMenuItemCreator;
            _xMenuItem        = xMenuItemCreator();
            Windows           = windows;
        }

        public IEnumerable < Lazy < Window > > Windows { get; }

        public IMenuItem GetXMenuItem()
        {
            var root = _xMenuItem;
            _xMenuItem.Header = "Windows";
            _xMenuItem.Children = Windows.Select(
                                                 (
                                                     lazy,
                                                     i
                                                 ) => {
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