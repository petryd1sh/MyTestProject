using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Core;
using MyTestProject.Services;

namespace MyTestProject.Tests;

public class AnotherJsonPlaceholderTestFixture : ITestFixture
{
    public void ConfigureServices(IServiceCollection services)
    {
        this.Build<JsonPlaceholderTestFixture>(services);
        services.AddTransient<ICommentService, CommentServiceNotImplemented>();
    }
}