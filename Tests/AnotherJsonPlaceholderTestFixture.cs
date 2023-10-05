using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Core;
using MyTestProject.Services;

namespace MyTestProject.Tests;

public class AnotherJsonPlaceholderTestFixture : ITestFixture
{
    public void ConfigureServices(IServiceCollection services)
    {
        //new JsonPlaceholderTestFixture().ConfigureServices(services);
        JsonPlaceholderTestFixture.Create.ConfigureServices(services);
        services.AddTransient<ICommentService, CommentServiceNotImplemented>();
    }
}