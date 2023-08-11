using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Api;
using MyTestProject.Core;
using MyTestProject.Core.Config;
using MyTestProject.Core.RestEase;
using MyTestProject.Services;
using NUnit.Framework.Internal;
using RestEase.HttpClientFactory;

namespace MyTestProject.Tests;

public class JsonPlaceholderTestFixture : ITestFixture
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<SomeHandler>();
        services.AddRestEaseClient<ICommentsApi>(TestConfig.GetTestRunParameter("baseUrl")).AddHttpMessageHandler<SomeHandler>();
        //services.AddRestEaseClient<ICommentsApi>(TestConfig.GetEnvironmentVariable("baseUrl")).AddHttpMessageHandler<SomeHandler>();
        services.AddTransient<ICommentService, CommentService>();
    }
}
