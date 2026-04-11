using Markdig;
using System.Text.RegularExpressions;

namespace Portfolio.Web.Services;

public enum ContentBlockType
{
    Header,
    Section
}
public class ContentBlock
{
    public ContentBlockType Type { get; set; } = ContentBlockType.Section;
    public string Content { get; set; } = string.Empty;
}

public class MarkdownService
{
    private readonly HttpClient _httpClient;
    private readonly MarkdownPipeline _pipeline;

    public MarkdownService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();
    }

    public async Task<List<ContentBlock>> ParseMarkdownToBlocksAsync(string markdownPath)
    {
        var markdown = await _httpClient.GetStringAsync(markdownPath);
        return ParseMarkdownToBlocks(markdown);
    }

    public List<ContentBlock> ParseMarkdownToBlocks(string markdown)
    {
        var blocks = new List<ContentBlock>();
        
        // Jaetaan H1 ja H2 otsikoiden perusteella
        var lines = markdown.Split('\n');
        var currentContent = new List<string>();

        foreach (var line in lines)
        {
            // Jos H1, tee siitä oma kortti
            if (line.Trim().StartsWith("# ") && !line.Trim().StartsWith("## "))
            {
                // Tallenna edellinen sisältö jos on
                if (currentContent.Count > 0)
                {
                    var html = Markdown.ToHtml(string.Join("\n", currentContent), _pipeline);
                    blocks.Add(new ContentBlock { Type = ContentBlockType.Section, Content = html });
                    currentContent.Clear();
                }

                // Lisää H1 omana korttinaan
                var h1Html = Markdown.ToHtml(line, _pipeline);
                blocks.Add(new ContentBlock { Type = ContentBlockType.Header, Content = h1Html });
            }
            // Jos H2, tallenna edellinen ja aloita uusi sektio
            else if (line.Trim().StartsWith("## "))
            {
                // Tallenna edellinen sisältö
                if (currentContent.Count > 0)
                {
                    var html = Markdown.ToHtml(string.Join("\n", currentContent), _pipeline);
                    blocks.Add(new ContentBlock { Type = ContentBlockType.Section, Content = html });
                    currentContent.Clear();
                }

                // Aloita uusi sektio H2:lla
                currentContent.Add(line);
            }
            else
            {
                currentContent.Add(line);
            }
        }

        // Lisää viimeinen sektio
        if (currentContent.Count > 0)
        {
            var html = Markdown.ToHtml(string.Join("\n", currentContent), _pipeline);
            blocks.Add(new ContentBlock { Type = ContentBlockType.Section, Content = html });
        }

        return blocks;
    }
}
