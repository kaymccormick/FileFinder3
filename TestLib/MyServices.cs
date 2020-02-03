using AppShared ;
using AppShared.Infos ;

namespace TestLib
{
    public class MyServices : IMyServices
    {
        public MyServices ( InfoContext.Factory infoContextFactory )
        {
            InfoContextFactory = infoContextFactory ;
        }

        public InfoContext.Factory InfoContextFactory { get ; }
    }
}