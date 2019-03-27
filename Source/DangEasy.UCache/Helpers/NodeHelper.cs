using Umbraco.Web;

namespace DangEasy.UCache.Helpers
{
    internal class NodeHelper
    {
        internal static int EnsureNodeId(int ? nodeId)
        {
            int id;
            if (nodeId != null && nodeId.Value != 0)
            {
                id = nodeId.Value;
            }
            else
            {
                // TODO: how are we going to abstract this???
                id =  UmbracoContext.Current.PageId.Value;
            }

            return id;
        }
    }
}
