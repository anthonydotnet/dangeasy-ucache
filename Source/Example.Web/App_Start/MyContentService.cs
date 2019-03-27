using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Example.Web.App_Start
{
    public class MyContentService
    {
        public static IPublishedContent GetHomeNode(int rootNodeId)
        {
                var helper = new UmbracoHelper(UmbracoContext.Current);
                var node = helper.TypedContentAtRoot().First();
                return node;
        }
    }
}