using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MainVie
    {
        public IEnumerable < ITabGuest > Guests { get ; }

        public MainVie (IEnumerable <ITabGuest> guests) { Guests = guests ; }
    }

    public interface ITabGuest
    {
    }
}
