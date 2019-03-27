using Umbraco.Web;
using Umbraco.Core;
using DangEasy.Caching.MemoryCache;
using DangEasy.UCache.Resolvers;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;
using DangEasy.UCache.Services;
using DangEasy.UCache.Helpers;
using System.Linq;

namespace DangEasy.UCache
{
    public interface IUCache
    {
        void RegisterSiteNodeContentTypeAlias(string contentTypeAlias);
        void RegisterSingle(string alias, string xpath);
        void RegisterSingle(string alias, Func<int, IPublishedContent> hydrationFunction);
        void RegisterCollection(string alias, string xpath);
        void RegisterCollection(string alias, Func<int, IPublishedContent> hydrationFunction);

        IPublishedContent Get(string alias);
        IPublishedContent Get(string alias, int rootNodeId);

        IEnumerable<IPublishedContent> Fetch(string alias);
        IEnumerable<IPublishedContent> Fetch(string alias, int rootNodeId);
    }

    public class UCache : IUCache
    {
        internal Dictionary<string, string> _xPaths;
        internal Dictionary<string, Func<int, IPublishedContent>> _hydrationFunctions;
        INodeGetter _nodeGetter;
        string _siteNodeContentTypeAlias;

        #region Instance
        private static UCache _instance;
        public static IUCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UCache();
                }

                return _instance;
            }
        }

        private UCache()
        {
            _nodeGetter = Resolver.NodeGetter();
            _xPaths = new Dictionary<string, string>();
            _hydrationFunctions = new Dictionary<string, Func<int, IPublishedContent>>();
        }
        #endregion



        #region Registration

        public void RegisterSiteNodeContentTypeAlias(string contentTypeAlias)
        {
            _siteNodeContentTypeAlias = contentTypeAlias;
        }

        public void RegisterSingle(string alias, string xpath)
        {
            _xPaths.Add(alias, xpath);
        }

        public void RegisterSingle(string alias, Func<int, IPublishedContent> hydrationFunction)
        {
            _hydrationFunctions.Add(alias, hydrationFunction);
        }

        public void RegisterCollection(string alias, string xpath)
        {
            _xPaths.Add(alias, xpath);
        }


        public void RegisterCollection(string alias, Func<int, IPublishedContent> hydrationFunction)
        {
            _hydrationFunctions.Add(alias, hydrationFunction);
        }

        #endregion



        // Get
        public IPublishedContent Get(string alias) 
        {
            var rootNode = GetRootNode(0);

            return Get(alias, rootNode.Id);
        }

        public  IPublishedContent Get(string alias, int rootNodeId) 
        {
            IPublishedContent result;

            if (_xPaths.ContainsKey(alias))
            {
                var xpath = _xPaths[alias];
                result = _nodeGetter.Get(xpath, rootNodeId);

                return result;
            }

            if (_hydrationFunctions.ContainsKey(alias))
            {
                var function = _hydrationFunctions[alias];
                result = _nodeGetter.Get(function, alias, rootNodeId);

                return result;
            }

            return default(IPublishedContent);
        }

       

        // Fetch
        public IEnumerable<IPublishedContent> Fetch(string alias) 
        {
            var rootNode = GetRootNode(0);

            return  Fetch(alias, rootNode.Id);
        }


        public IEnumerable<IPublishedContent> Fetch(string alias, int rootNodeId) 
        {
            IEnumerable<IPublishedContent> result;

            if (_xPaths.ContainsKey(alias))
            {
                var xpath = _xPaths[alias];
                result = _nodeGetter.Fetch(xpath, rootNodeId);

                return result;
            }

            if (_hydrationFunctions.ContainsKey(alias))
            {
                var function = _hydrationFunctions[alias] as Func<int, IEnumerable<IPublishedContent>>;
                result = _nodeGetter.Fetch(function, alias, rootNodeId);

                return result;
            }

            return default(IEnumerable<IPublishedContent>);
        }




        //--
        // Where the magic happens!
        //--
        internal IPublishedContent GetRootNode(int nodeId)
        {
            var cacheKey = CacheKey.Build<NodeGetterCachedProxy, Dictionary<int, IPublishedContent>>("RootNodes");
            var sites = Cache.Instance.Get<Dictionary<int, IPublishedContent>>(cacheKey);

            IPublishedContent rootNode;
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            if (sites != null)
            {
                var node = umbracoHelper.TypedContent(NodeHelper.EnsureNodeId(nodeId));

                // Use the IDs in the Path property to get the Root node from the cached Dictionary.
                var pathIds = node.Path.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));

                foreach (var id in pathIds)
                {
                    if (sites.ContainsKey(id))
                    {
                        rootNode = sites[id];

                        return rootNode;
                    }
                }
            }

            // get here if there were no cached Site nodes, OR the Site node was not found in the dictionary
            sites = new Dictionary<int, IPublishedContent>();


            // this needs to be configurable by node type alias because the "site node" migght be at level 2
            if (!string.IsNullOrWhiteSpace(_siteNodeContentTypeAlias))
            {
                rootNode = umbracoHelper.TypedContent(NodeHelper.EnsureNodeId(nodeId)).AncestorOrSelf(_siteNodeContentTypeAlias); // top level node
            }
            else
            {
                rootNode = umbracoHelper.TypedContent(NodeHelper.EnsureNodeId(nodeId)).AncestorOrSelf(1);
            }

            if (rootNode != null)
            {
                // GetSiteNode might return null
                sites.Add(rootNode.Id, rootNode);
                Cache.Instance.Add(cacheKey, sites);
            }


            return rootNode;
        }

      
    }
}
