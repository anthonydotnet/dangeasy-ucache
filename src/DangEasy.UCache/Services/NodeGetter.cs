using DangEasy.UCache.Adaptors;
using Umbraco.Web;
using Umbraco.Core.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DangEasy.UCache.Services
{
    internal class NodeGetter : INodeGetter
    {
        IUmbracoHelperAdaptor _umbracoHelper;

        internal NodeGetter(IUmbracoHelperAdaptor umbracoHelperAdaptor)
        {
            _umbracoHelper = umbracoHelperAdaptor;
        }

        // Get
        public IPublishedContent Get(string xpath, int rootNodeId)
        {
            var nodes = _umbracoHelper.TypedContentByXPath(xpath);
            var node = _umbracoHelper.TypedContent(rootNodeId);
            var filtered = nodes.Where(x => IsInTree(x, node)).ToList();

            return filtered.FirstOrDefault();
        }


        public IPublishedContent Get(Func<int, IPublishedContent> hydrationFunction, string functionAlias, int rootNodeId)
        {
            var res = hydrationFunction(rootNodeId);
            return res;
        }


        // Fetch
        public IEnumerable<IPublishedContent> Fetch(string xpath, int rootNodeId)
        {
            var nodes = _umbracoHelper.TypedContentByXPath(xpath);
            var node = _umbracoHelper.TypedContent(rootNodeId);
            var filtered = nodes.Where(x => IsInTree(x, node)).ToList();

            return filtered;
        }


        public IEnumerable<IPublishedContent> Fetch(Func<int, IEnumerable<IPublishedContent>> hydrationFunction, string functionAlias, int rootNodeId)
        {
            var res = hydrationFunction(rootNodeId);
            return res;
        }



        //--
        // IPublishedContentree crawling
        //--
        protected bool IsInTree(IPublishedContent node1, IPublishedContent node2)
        {
            var path = node1.Path.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

            if (path.Contains(node2.Id))
            {
                return true;
            }

            path = node2.Path.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            if (path.Contains(node1.Id))
            {
                return true;
            }

            return false;
        }

    }
}
