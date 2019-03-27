using DangEasy.Caching.MemoryCache;
using DangEasy.UCache.Services;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Core.Publishing;
using Umbraco.Core.Events;
using Umbraco.Core.Models;

namespace DangEasy.Umbraco.Geto.SIPublishedContentarIPublishedContentup
{
    public class UCacheStartup : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentService.Published += ContentService_Published;
            ContentService.UnPublished += ContentService_UnPublished; 
            ContentService.Moved += ContentService_Moved;
            ContentService.Trashed += ContentService_Trashed;
        }

        private void ContentService_Trashed(IContentService sender, MoveEventArgs<IContent> e)
        {
            ClearCache();
        }

        private void ContentService_Moved(IContentService sender, MoveEventArgs<IContent> e)
        {
            ClearCache();
        }

        private void ContentService_UnPublished(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            ClearCache();
        }

        private void ContentService_Published(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            ClearCache();
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        private   void ClearCache()
        {
            Cache.Instance.RemoveByPrefix(typeof(NodeGetterCachedProxy).ToString());
        }
    }
}
