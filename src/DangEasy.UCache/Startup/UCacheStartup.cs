using DangEasy.Caching.MemoryCache;
using DangEasy.UCache.Services;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Core.Publishing;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Cache;
using Umbraco.Core.Sync;
using Umbraco.Core.Cache;

namespace DangEasy.UCache.Startup
{
    public class UCacheStartup : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            PageCacheRefresher.CacheUpdated += PageCacheRefresher_CacheUpdated;
        }

        private void PageCacheRefresher_CacheUpdated(PageCacheRefresher sender, CacheRefresherEventArgs e)
        {
            Cache.Instance.RemoveByPrefix(typeof(NodeGetterCachedProxy).ToString());
        }
        

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }
    }
}
