using Microsoft.Extensions.DependencyInjection;

namespace MyTestProject.Core;

public static class TestFixtureExtensions
{
    public static void Build<T>(this ITestFixture testFixture, IServiceCollection services) where T : ITestFixture, new()
    {
        Console.WriteLine($"Building TestFixture {typeof(T)}");
        new T().ConfigureServices(services);
    }
}