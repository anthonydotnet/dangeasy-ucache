using DangEasy.UCache;
using Umbraco.Core;

namespace ExampleUmbV73.Web
{
    public class ExampleCacheRegistration : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // tell ucache your site node doctype
        //    UCache.Instance.RegisterSiteNodeContentTypeAlias("home"); // will default to top level nodes if not set

            UCache.Instance.RegisterSingle("homepage", "//Home");
            UCache.Instance.RegisterCollection("blogPosts", "//Home/BlogPostRepository/BlogPost");

            // register with a function
            UCache.Instance.RegisterSingle("blog", (rootNodeId) => ExampleContentService.GetBlogLandingNode(rootNodeId));
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }
    }
}