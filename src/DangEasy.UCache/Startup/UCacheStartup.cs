using DangEasy.Caching.MemoryCache;
using DangEasy.UCache.Services;
using Umbraco.Core;
using Umbraco.Web.Cache;
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
