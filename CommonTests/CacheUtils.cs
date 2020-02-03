#region header
// Kay McCormick (mccor)
// 
// FileFinder3
// CommonTests
// CacheUtils.cs
// 
// 2020-02-03-12:28 AM
// 
// ---
#endregion
using System ;
using System.Linq ;
using System.Reactive.Concurrency ;
using System.Reactive.Linq ;
using System.Windows.Threading ;
using Common.Logging ;

namespace CommonTests
{
    public static class CacheUtils
    { 

        public static void SetupCacheSubscriber ( )
        {
            var myCacheTarget = MyCacheTarget.GetInstance ( 1000 ) ;
            myCacheTarget.Cache.SubscribeOn ( Scheduler.Default )
                         .Buffer ( TimeSpan.FromMilliseconds ( 100 ) )
                         .Where ( x => x.Any ( ) )
                         .ObserveOnDispatcher ( DispatcherPriority.Background )
                         .Subscribe (
                                     infos => {
                                         foreach ( var logEventInfo in infos )
                                         {
                                                                                      }
                                     }
                                    ) ;
        }
    }
}