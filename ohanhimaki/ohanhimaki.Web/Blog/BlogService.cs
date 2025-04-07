using System.Net.Http.Json;
using Markdig;
using MarkdownService.Services;
using YamlDotNet.Serialization;

public class BlogService
{
     private readonly MarkdownService.Services.MarkdownService _markdownService;
    private readonly HttpClient _http;
    private Dictionary<string, BlogPost>? _posts;
    

    public BlogService(HttpClient http, MarkdownService.Services.MarkdownService markdownService)
    {
        _http = http;
        _markdownService = markdownService;
    }

    public async Task<IEnumerable<BlogPost>> GetAllPostsAsync()
    {
        if (_posts is not null) return _posts.Values.OrderByDescending(x => x.Date);

        var parsed = await _markdownService.GetAllAsync<BlogPostFrontMatter>("blog/index.json", "blog");

        var tmpPosts = new Dictionary<string, BlogPost>();

        foreach (var doc in parsed)
        {
            tmpPosts[doc.File] = new BlogPost
            {
                Title = doc.Metadata.Title,
                Date = doc.Metadata.Date,
                Tags = doc.Metadata.Tags,
                Content = doc.HtmlContent
            };
        }

        _posts = tmpPosts;
        return _posts.Values.OrderByDescending(x => x.Date);
    }

private BlogPost ParseMarkdown(string markdown)
{
    var (meta, html) = MarkdownReader.MarkdownParser.Parse<BlogPostFrontMatter>(markdown);

    return new BlogPost
    {
        Title = meta.Title,
        Date = meta.Date,
        Tags = meta.Tags,
        Content = html
    };
}


public class BlogPostFrontMatter
{
    public string Title { get; set; } = "";
    public DateTime Date { get; set; }
    public List<string> Tags { get; set; } = new();
}

    // public IEnumerable<BlogPost> GetAllPosts() => _posts.Values.OrderByDescending(p => p.Date);
    // public Task<IEnumerable<BlogPost>> GetAllPostsAsync() => Task.FromResult(_posts.Values.OrderByDescending(p => p.Date));
}
