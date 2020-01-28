using System ;
using System.Collections.Generic ;
using System.Windows ;
using System.Windows.Input ;
using AppShared.Interfaces ;
using Autofac.Features.OwnedInstances ;
using NLog ;

namespace Common.Menus
{
    public class XMenuItem : IMenuItem
    {
        private ILogger Logger { get;  }

        public XMenuItem(Owned <Func <Type, ILogger> > func)
        {
	        Logger = func.Value( typeof(XMenuItem) );
        }
        

        public string Header { get; set; }

        public IEnumerable < IMenuItem > Children { get; set; }
        
        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public IInputElement CommandTarget { get; set; }

        public override string ToString()
        {
            return
                $"{nameof( Header )}: {Header}, {nameof( Children )}: {Children}, {nameof( Command )}: {Command}, {nameof( CommandParameter )}: {CommandParameter}, {nameof( CommandTarget )}: {CommandTarget}";
        }
    }
}