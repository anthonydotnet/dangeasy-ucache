using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace DangEasy.UCache.Adaptors
{
    public interface IUmbracoHelperAdaptor
    {
        IPublishedContent TypedMedia(int id);
        IPublishedContent TypedContent(int id);
        IPublishedContent TypedContentAtXPathSingle(string xpath);
        IEnumerable<IPublishedContent> TypedContentByXPath(string xpath);
        IEnumerable<IPublishedContent> TypedContentAtRoot();
    }

    public class UmbracoHelperAdaptor : IUmbracoHelperAdaptor
    {
        UmbracoHelper _umbracoHelper;

        public UmbracoHelperAdaptor(UmbracoHelper umbracoHelper)
        {
            _umbracoHelper = umbracoHelper;
        }

        public IPublishedContent TypedMedia(int id)
        {
            return _umbracoHelper.TypedMedia(id);
        }

        public IPublishedContent TypedContent(int id)
        {
            return _umbracoHelper.TypedContent(id);
        }

        public IPublishedContent TypedContentAtXPathSingle(string xpath)
        {
            return _umbracoHelper.TypedContentSingleAtXPath(xpath);
        }

        public IEnumerable<IPublishedContent> TypedContentByXPath(string xpath)
        {
            return _umbracoHelper.TypedContentAtXPath(xpath);
        }

        public IEnumerable<IPublishedContent> TypedContentAtRoot()
        {
            return _umbracoHelper.TypedContentAtRoot();
        }
    }
}
