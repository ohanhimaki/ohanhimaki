using MarkdownService.Models;

namespace MarkdownService.Interfaces;

public interface IMarkdownReader
{
    Task<IEnumerable<MarkdownFile>> GetAllAsync();
    Task<MarkdownFile?> GetBySlugAsync(string slug);
}
