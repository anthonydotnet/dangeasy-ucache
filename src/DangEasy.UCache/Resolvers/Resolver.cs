using DangEasy.Caching.MemoryCache;
using DangEasy.UCache.Adaptors;
using DangEasy.UCache.Services;
using System.Configuration;
using Umbraco.Web;

namespace DangEasy.UCache.Resolvers
{
    internal class Resolver
    {
        const string AppSettingKey = "UCache:Enabled";


        internal static INodeGetter NodeGetter()
        {
            var cacheKey = CacheKey.Build<Resolver, NodeGetter>("NodeGetterInstance");

            var nodeGetter = Cache.Instance.Get<INodeGetter>(cacheKey, () => 
            {
                var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
                var umbAdaptor = new UmbracoHelperAdaptor(umbracoHelper);
                var getter = new NodeGetter(umbAdaptor);

                if (ConfigurationManager.AppSettings[AppSettingKey] == true.ToString().ToLower())
                {
                    return new NodeGetterCachedProxy(getter);
                }

                return getter;
            });

            return nodeGetter;
        }
    }
}
