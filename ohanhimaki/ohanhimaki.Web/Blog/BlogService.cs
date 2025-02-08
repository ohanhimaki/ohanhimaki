using System.Net.Http.Json;
using Markdig;
using YamlDotNet.Serialization;

public class BlogService
{
    private readonly HttpClient _http;
    private readonly Dictionary<string, BlogPost> _posts = new();

    public BlogService(HttpClient http)
    {
        _http = http;
    }

    public async Task InitializeAsync()
    {
        var files = await _http.GetFromJsonAsync<List<string>>("blog/index.json");
        if (files is null) return;

        foreach (var file in files)
        {
            var markdown = await _http.GetStringAsync($"blog/{file}");
            var post = ParseMarkdown(markdown);
            _posts[file] = post;
        }
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

    public IEnumerable<BlogPost> GetAllPosts() => _posts.Values.OrderByDescending(p => p.Date);
}
