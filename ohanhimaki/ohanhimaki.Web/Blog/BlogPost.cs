public class BlogPost
{
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public List<string> Tags { get; set; } = new();
    public string Content { get; set; } = string.Empty;
}
