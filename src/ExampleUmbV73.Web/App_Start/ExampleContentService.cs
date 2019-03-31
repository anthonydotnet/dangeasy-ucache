using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;



namespace ExampleUmbV73.Web
{
    public class ExampleContentService
    {
        internal static IPublishedContent GetBlogLandingNode(int rootNodeId)
        {
            var helper = new UmbracoHelper(UmbracoContext.Current);

            var root = helper.TypedContent(rootNodeId);

            var blogLanding = root.Children.Where(x=>x.ContentType.Alias == "BlogPostRepository").FirstOrDefault();

            return blogLanding;
        }
    }
}