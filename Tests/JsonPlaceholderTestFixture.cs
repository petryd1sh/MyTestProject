using System.Net;
using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Api;
using MyTestProject.Core;
using MyTestProject.Core.Config;
using MyTestProject.Core.RestEase;
using MyTestProject.Services;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using RestEase.HttpClientFactory;

namespace MyTestProject.Tests;

public class AnotherJsonPlaceholderTestFixture : ITestFixture
{
    public void ConfigureServices(IServiceCollection services)
    {
        new JsonPlaceholderTestFixture().ConfigureServices(services);
        services.AddTransient<ICommentService, CommentService2>();
    }
}

public class JsonPlaceholderTestFixture : ITestFixture
{
    private readonly List<IAsyncPolicy<HttpResponseMessage>> _asyncPolicies = GetAsyncPolicies();
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddSingleton<LoggingHandler>(); // default is no response output
        services.AddSingleton<LoggingHandler>(_ => new LoggingHandler(true));
        services.AddRestEaseClient<ICommentsApi>(TestConfig.GetTestRunParameter("baseUrl"))
            .AddPolicyHandlers(_asyncPolicies)
            .AddLoggingHandler();
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

public static class MyTestFixtures
{
    public static readonly List<Type> FixtureServicesList = new()
    {
        typeof(JsonPlaceholderTestFixture),
        typeof(JsonPlaceholderTestFixture)
    };
}