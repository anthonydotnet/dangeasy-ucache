using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Example.Web.App_Start
{
    public class ExampleContentService
    {
        internal static IPublishedContent GetBlogLandingNode(int rootNodeId)
        {
            var helper = new UmbracoHelper(UmbracoContext.Current);

            var root = helper.TypedContent(rootNodeId);

            var blogLanding = root.Children("blog").FirstOrDefault();

            return blogLanding;
        }
    }
}