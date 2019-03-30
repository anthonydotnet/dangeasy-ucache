using DangEasy.Caching.MemoryCache;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;

namespace DangEasy.UCache.Services
{
    internal class NodeGetterCachedProxy : INodeGetter
    {
        NodeGetter _nodeGetter;

        internal NodeGetterCachedProxy(NodeGetter nodeGetter)
        {
            _nodeGetter = nodeGetter;
        }


        // Get
        public IPublishedContent Get(string xpath, int rootNodeId) 
        {
            var cacheKey = CacheKey.Build<NodeGetterCachedProxy, IPublishedContent>($"Get_{xpath}_{rootNodeId}");

            var node = Cache.Instance.Get(cacheKey, () => _nodeGetter.Get(xpath, rootNodeId));

            return node;
        }


        public IPublishedContent Get(Func<int, IPublishedContent> hydrationFunction, string functionAlias, int rootNodeId)
        {
            var cacheKey = CacheKey.Build<NodeGetterCachedProxy, IPublishedContent>($"GetByhydrationFunction_{functionAlias}_{rootNodeId}");

            var node = Cache.Instance.Get(cacheKey, () => _nodeGetter.Get(hydrationFunction, null, rootNodeId));

            return node;
        }


        // Fetch
        public IEnumerable<IPublishedContent> Fetch(string xpath, int rootNodeId) 
        {
            var cacheKey = CacheKey.Build<NodeGetterCachedProxy, IPublishedContent>($"Fetch_{xpath}_{rootNodeId}");

            var node = Cache.Instance.Get(cacheKey, () => _nodeGetter.Fetch(xpath, rootNodeId));

            return node;
        }

    
        public IEnumerable<IPublishedContent> Fetch(Func<int, IEnumerable<IPublishedContent>> hydrationFunction, string functionAlias, int rootNodeId) 
        {
            var cacheKey = CacheKey.Build<NodeGetterCachedProxy, IPublishedContent>($"GetByhydrationFunction_{functionAlias}_{rootNodeId}");

            var node = Cache.Instance.Get(cacheKey, () => _nodeGetter.Fetch(hydrationFunction, null,  rootNodeId));

            return node;
        }
    }
}
