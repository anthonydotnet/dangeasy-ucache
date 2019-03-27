using Umbraco.Core.Models;
using System.Collections.Generic;
using System;

namespace DangEasy.UCache.Services
{
    internal interface INodeGetter
    {
        IPublishedContent Get(string xpath, int rootNodeId);
        IPublishedContent Get(Func<int, IPublishedContent> hydrationFunction, string functionAlias, int rootNodeId);

        IEnumerable<IPublishedContent> Fetch(string xpath, int rootNodeId);
        IEnumerable<IPublishedContent> Fetch(Func<int, IEnumerable<IPublishedContent>> hydrationFunction, string functionAlias, int rootNodeId);
    }
}
