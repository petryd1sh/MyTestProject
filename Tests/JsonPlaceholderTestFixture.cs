using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using MyTestProject.Api;
using MyTestProject.Core;
using MyTestProject.Core.Config;
using MyTestProject.Core.RestEase;
using MyTestProject.Services;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using RestEase;
using RestEase.HttpClientFactory;

namespace MyTestProject.Tests;

public class JsonPlaceholderTestFixture : ITestFixture
{
    public void ConfigureServices(IServiceCollection services)
    {
        var policies = GetAsyncPolicies();

        //services.AddSingleton<LoggingHandler>(); // default is no response content output
        services.AddSingleton<LoggingHandler>(_ => new LoggingHandler(true));
        // services.AddRestEaseClient<ICommentsApi>(TestConfig.GetTestRunParameter("baseUrl"))
        //     .AddPolicyHandlers(policies)
        //     .AddHttpMessageHandler<LoggingHandler>();
        services.AddRestEaseClient<ICommentsApi>(TestConfig.GetTestRunParameter("baseUrl"))
            .AddPolicyHandlers(policies)
            .AddLoggingHandler(); // added extension methods
        services.AddTransient<ICommentService, CommentService>();
    }

    private static List<IAsyncPolicy<HttpResponseMessage>> GetAsyncPolicies()
    {
        var policies = new List<IAsyncPolicy<HttpResponseMessage>>();
        var policy = Policy
            .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound)
            .RetryAsync();
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>() // thrown by Polly's TimeoutPolicy if the inner call times out
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            });
        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10); // Timeout for an individual try
        
        policies.Add(policy);
        policies.Add(retryPolicy);
        policies.Add(timeoutPolicy);
        
        return policies;
    }
}
