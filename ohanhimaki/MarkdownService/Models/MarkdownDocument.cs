namespace MarkdownService.Models;


public class MarkdownDocument<TMetadata>
{
    public string File { get; set; } = "";
    public TMetadata Metadata { get; set; } = default!;
    public string HtmlContent { get; set; } = "";
}
