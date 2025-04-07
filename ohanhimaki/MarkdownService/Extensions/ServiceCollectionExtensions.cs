using MarkdownService.Interfaces;
using MarkdownService.Models;
using MarkdownService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MarkdownService.Extensions;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMarkdownService(this IServiceCollection services, Action<MarkdownOptions> configure)
    {
        services.Configure(configure);
        services.AddSingleton<IMarkdownReader, MarkdownReader>();
        return services;
    }
}
