using DangEasy.UCache;
using Umbraco.Core;

namespace Example.Web.App_Start
{
    public class ExampleCacheRegistration : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // tell ucache your site node doctype
            UCache.Instance.RegisterSiteNodeContentTypeAlias("home"); // will default to top level nodes if not set

            UCache.Instance.RegisterSingle("homepage", "//home");
            UCache.Instance.RegisterCollection("blogPosts", "//home/blog/blogpost");

            // register with a function
            UCache.Instance.RegisterSingle("blog", (rootNodeId) => ExampleContentService.GetBlogLandingNode(rootNodeId));
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }
    }
}