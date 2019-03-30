# dangeasy-ucache

A caching package for Umbraco CMS. Automatically caches result of registered tree crawling and xpath results.

# Installation
Use NuGet to install the [package](https://www.nuget.org/packages/DangEasy.UCache/).
```
PM> Install-Package DangEasy.UCache
```

## Examples
### Enabling UCache
Add this setting to your web.config.
```
<add key="UCache:Enabled" value="true" />
```

### Registering content
```
// tell UCache your site node doctype
UCache.Instance.RegisterSiteNodeContentTypeAlias("site"); // will default to top level nodes if not set

// register with xpath
UCache.Instance.RegisterSingle("homepage", "//home");
UCache.Instance.RegisterCollection("blogPosts", "//home/blog/blogpost");

// register with a function
UCache.Instance.RegisterSingle("blogLanding", (rootNodeId) => ExampleContentService.GetBlogLandingNode(rootNodeId));
```

### Retreiving content
```
var home = UCache.Instance.Get("homepage"); 
var posts = UCache.Instance.Fetch("blogPosts");
var blog = UCache.Instance.Get("blogLanding") as Blog;
```

## Cache Clearing Events
Cache clearing is aggressive. All content which is registered will be cleared on the following events. 
- Published
- Unpublished
- Moved
- Trashed
