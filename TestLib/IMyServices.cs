#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// WpfApp1Tests3
// IMyServices.cs
// 
// 2020-02-02-5:22 PM
// 
// ---
#endregion
using AppShared ;
using AppShared.Infos ;

namespace TestLib
{
    public interface IMyServices

    {
        InfoContext.Factory InfoContextFactory { get ; }
    }
}