using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    public class XMenuItem : ICommandSource
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private IEnumerable<XMenuItem> _children;

        public string Header { get; set; }

        public IEnumerable<XMenuItem> Children
        {
            get
            {
                
                Logger.Debug($"here: {Environment.StackTrace}");
                try
                {
                    throw new Exception();
                }
                catch (Exception e)
                {
                    Logger.Debug(e, e.StackTrace);
                }

                return _children;
            }
            set { _children = value; }
        }

        public ICommand Command { get; set; }
        public object CommandParameter { get; set;  }
        public IInputElement CommandTarget { get; set;  }

        public override string ToString()
        {
            return $"{nameof(Header)}: {Header}, {nameof(Children)}: {Children}, {nameof(Command)}: {Command}, {nameof(CommandParameter)}: {CommandParameter}, {nameof(CommandTarget)}: {CommandTarget}";
        }
    }
}