using ohanhimaki.Web.Components;

namespace ohanhimaki.Web.Layout;

public class PagesService
{
    private List<BlogPost>? _posts;

    private readonly Dictionary<int, Type> _hardcodedPages = new()
    {
        { 100, typeof(HomePage) },
        { 200, typeof(BlogList) },
        { 300, typeof(GitHubPage) }
    };

    private readonly BlogService _blogService;

    public PagesService(BlogService blogService)
    {
        
        _blogService = blogService;
        // _posts = blogService.GetAllPostsAsync().Result.ToList();
        
        // _hardcodedPages = new Dictionary<int, Type>
        // {
        //     { 100, typeof(HomePage) },
        //     { 200, typeof(BlogList) },
        //     { 300, typeof(GitHubPage) }
        // };
    }
    
    
    public async Task InitializeAsync()
    {
        _posts = (await _blogService.GetAllPostsAsync()).ToList();
    }

    public Type? GetPageComponent(int pageNumber)
    {
        if (_hardcodedPages.TryGetValue(pageNumber, out var component))
        {
            return component;
        }
        else if (pageNumber >= 201 && (200 + _posts.Count) >= pageNumber)
        {
            return typeof(BlogPostPage);
        }

        return typeof(HomePage); // Default page
    }
    
    public List<int> GetValidPages()
    {
        var valid = _hardcodedPages.Keys.ToList();
        for (int i = 0; i < _posts.Count; i++)
        {
            valid.Add(201 + i);
        }

        return valid.OrderBy(x => x).ToList();
    }
}