# dangeasy-ucache

A caching package for Umbraco CMS to allow better performance. Caches tree crawling and xpath results.

## Examples
### Registering content
```
UCache.Instance.RegisterSingle("homepage", "//home");
UCache.Instance.RegisterCollection("blogPosts", "//home/blog/blogpost");

UCache.Instance.RegisterSingle("homeNode", (rootNodeId) => MyContentService.GetHomeNode(rootNodeId));
```

### Retreiving content
```
var root = UCache.Instance.Get("homeNode"); 
var posts = UCache.Instance.Fetch("blogPosts");
```

## Cache Clearing Events
Cache clearing is aggressive. All content which is registered will be cleared on the following events. 
- Published
- Unpublished
- Moved
- Trashed
