using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autofac.Features.Metadata;
using WpfApp1.Commands;
using WpfApp1.Interfaces;

namespace WpfApp1.Menus
{
    public class WindowsTopLevelMenu : ITopLevelMenu
    {
        private readonly IMenuItem          _xMenuItem;
        private readonly Func < IMenuItem > _xMenuItemCreator;

        public WindowsTopLevelMenu(
            IEnumerable < Meta < Lazy < Window > > > windows,
            Func < IMenuItem >              xMenuItemCreator
        )
        {
            _xMenuItemCreator = xMenuItemCreator;
            _xMenuItem        = xMenuItemCreator();
            Windows           = windows;
        }

        public IEnumerable < Meta<Lazy < Window > > > Windows { get; }

        public IMenuItem GetXMenuItem()
        {
            var root = _xMenuItem;
            _xMenuItem.Header = "Windows";
            _xMenuItem.Children = Windows.Select(
                                                 Selector
                                                ).ToList();
            return root;
        }

        private IMenuItem Selector(
            Meta<Lazy<Window >> window,
            int             i
        )
        {
            var m = _xMenuItemCreator();
            m.Header = (string)window.Metadata["WindowTitle"];
            m.Command          = MyAppCommands.OpenWindow;
            m.CommandParameter = window;
            return m;
        }
    }
}