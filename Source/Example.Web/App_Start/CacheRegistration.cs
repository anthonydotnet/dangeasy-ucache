using DangEasy.UCache;
using System.Linq;
using Umbraco.Core;
using Umbraco.Web;

namespace Example.Web.App_Start
{
    public class CacheRegistration : IApplicationEventHandler
    {
        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            UCache.Instance.RegisterSingle("homepage", "//home");
            UCache.Instance.RegisterCollection("blogPosts", "//home/blog/blogpost");

            UCache.Instance.RegisterSingle("homeNode", (rootNodeId) => MyContentService.GetHomeNode(rootNodeId));


            //UCache.Instance.RegisterSingle("rootNode", (id) => {
            //    var helper = new UmbracoHelper(UmbracoContext.Current);
            //    var node = helper.TypedContentAtRoot().First();
            //    return node;
            //});

        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }
    }
}