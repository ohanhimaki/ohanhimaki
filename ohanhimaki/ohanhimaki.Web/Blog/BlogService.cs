using System.Net.Http.Json;
using Markdig;
using YamlDotNet.Serialization;

public class BlogService
{
    private readonly HttpClient _http;
    private Dictionary<string, BlogPost>? _posts;
    

    public BlogService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<BlogPost>> GetAllPostsAsync()
    {
        if (_posts is not null) return _posts.Values.OrderByDescending(x => x.Date);
        var files = await _http.GetFromJsonAsync<List<BlogPost>>("blog/index.json");
        
        var tmpPosts = new Dictionary<string, BlogPost>();

        foreach (var file in files)
        {
            var markdown = await _http.GetStringAsync($"blog/{file.File}");
            var post = ParseMarkdown(markdown);
            tmpPosts[file.File] = post;
        }
        _posts = tmpPosts;
        return _posts.Values.OrderByDescending(x => x.Date);
    }

    private BlogPost ParseMarkdown(string markdown)
    {
        var frontMatterEnd = markdown.IndexOf("---", 4);
        if (frontMatterEnd == -1) throw new Exception("Invalid front matter");

        var yaml = markdown.Substring(4, frontMatterEnd - 4);
        var content = markdown[(frontMatterEnd + 3)..];

        var deserializer = new DeserializerBuilder().Build();
        var metadata = deserializer.Deserialize<Dictionary<string, object>>(yaml);

        return new BlogPost
        {
            Title = metadata["title"].ToString(),
            Date = DateTime.Parse(metadata["date"].ToString()!),
            Tags = ((List<object>)metadata["tags"]).Select(t => t.ToString()!).ToList(),
            Content = Markdown.ToHtml(content)
        };
    }

    // public IEnumerable<BlogPost> GetAllPosts() => _posts.Values.OrderByDescending(p => p.Date);
    // public Task<IEnumerable<BlogPost>> GetAllPostsAsync() => Task.FromResult(_posts.Values.OrderByDescending(p => p.Date));
}
