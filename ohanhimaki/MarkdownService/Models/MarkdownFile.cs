namespace MarkdownService.Models;

    
public class MarkdownFile
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
}