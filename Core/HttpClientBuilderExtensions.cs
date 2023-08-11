using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Core.RestEase;
using Polly;

namespace MyTestProject.Core;

public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder AddPolicyHandlers(this IHttpClientBuilder httpClientBuilder,  List<IAsyncPolicy<HttpResponseMessage>> policies)
    {
        foreach (var policy in policies)
        {
            httpClientBuilder.AddPolicyHandler(policy);
        }

        return httpClientBuilder;
    }
    
    public static IHttpClientBuilder AddLoggingHandler(this IHttpClientBuilder httpClientBuilder)
    {
        httpClientBuilder.AddHttpMessageHandler<LoggingHandler>();
        return httpClientBuilder;
    }
}