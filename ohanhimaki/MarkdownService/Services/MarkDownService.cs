using System.Net.Http.Json;
using MarkdownService.Models;

namespace MarkdownService.Services;


public class MarkdownService
{
    private readonly HttpClient _http;

    public MarkdownService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<MarkdownDocument<T>>> GetAllAsync<T>(string indexPath, string basePath)
    {
        var index = await _http.GetFromJsonAsync<List<MarkdownIndexEntry>>(indexPath)
            ?? throw new Exception("Index file not found");

        var result = new List<MarkdownDocument<T>>();

        foreach (var entry in index)
        {
            var raw = await _http.GetStringAsync($"{basePath}/{entry.File}");
            var (meta, html) = MarkdownReader.MarkdownParser.Parse<T>(raw);

            result.Add(new MarkdownDocument<T>
            {
                File = entry.File,
                Metadata = meta,
                HtmlContent = html
            });
        }

        return result;
    }

    private class MarkdownIndexEntry
    {
        public string File { get; set; } = "";
    }
}
