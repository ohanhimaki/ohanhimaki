using Microsoft.Extensions.Configuration;
using Portfolio.Web.Services;

namespace portfolio.Test;

public class UnitTest1
{
    private const string exampleMarkdown = "# Header 1\nThis is a section under header 1.\n## Subheader 1.1\nThis is a section under subheader 1.1.\n# Header 2\nThis is a section under header 2.";

    [Fact]
    public void Test1()
    {
      var httpClient = new HttpClient();
      var config = new ConfigurationBuilder().Build();
      var markdownService = new MarkdownService(httpClient, config);

      var result = markdownService.ParseMarkdownToBlocks(exampleMarkdown);

      var headerCount = result.Count(b => b.Type == ContentBlockType.Header);
      var sectionCount = result.Count(b => b.Type == ContentBlockType.Section);
      Assert.Equal(3, result.Count);
      Assert.Equal(3, headerCount);
      Assert.Equal(2, sectionCount);

    }
}
