using System.Text.Json;
using MarkdownService.Interfaces;
using MarkdownService.Models;
using Microsoft.Extensions.Options;

using Markdig;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MarkdownService.Services;

public class MarkdownReader : IMarkdownReader
{
    private readonly string _rootFolder;
    private readonly string? _indexFile;

    public MarkdownReader(IOptions<MarkdownOptions> options)
    {
        _rootFolder = options.Value.MarkdownRootFolder;
        _indexFile = options.Value.IndexFile;
    }

    public async Task<IEnumerable<MarkdownFile>> GetAllAsync()
    {
        if (!string.IsNullOrEmpty(_indexFile))
        {
            // Lue index.json ja palauta lista
            var json = await File.ReadAllTextAsync(Path.Combine(_rootFolder, _indexFile));
            return JsonSerializer.Deserialize<List<MarkdownFile>>(json) ?? new();
        }
        else
        {
            // Lue kaikki .md-tiedostot hakemistosta
            var files = Directory.GetFiles(_rootFolder, "*.md");
            var result = new List<MarkdownFile>();
            foreach (var file in files)
            {
                var content = await File.ReadAllTextAsync(file);
                result.Add(new MarkdownFile
                {
                    Title = Path.GetFileNameWithoutExtension(file),
                    Slug = Path.GetFileNameWithoutExtension(file).ToLowerInvariant(),
                    Content = content,
                    Date = File.GetCreationTime(file)
                });
            }
            return result;
        }
    }

    public async Task<MarkdownFile?> GetBySlugAsync(string slug)
    {
        var all = await GetAllAsync();
        return all.FirstOrDefault(f => f.Slug == slug);
    }
    
    
public static class MarkdownParser
{
    public static (TMeta metadata, string htmlContent) Parse<TMeta>(string markdown)
    {
        var frontMatterStart = markdown.IndexOf("---");
        if (frontMatterStart != 0) throw new Exception("Front matter must start with '---'");

        var frontMatterEnd = markdown.IndexOf("---", 3);
        if (frontMatterEnd == -1) throw new Exception("Front matter not closed with '---'");

        var yaml = markdown.Substring(3, frontMatterEnd - 3).Trim();
        var content = markdown[(frontMatterEnd + 3)..].Trim();

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();

        var metadata = deserializer.Deserialize<TMeta>(yaml);
        var htmlContent = Markdown.ToHtml(content);

        return (metadata, htmlContent);
    }
}
}
