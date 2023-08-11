using System.Configuration;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MyTestProject.Core;

public class TestBase
{
    private ServiceProvider? ServiceProvider { get; set; }
    private ServiceCollection ServiceCollection { get; } = new();

    protected void Register<T>() where T : class, ITestFixture, new()
    {
        var collection = new T();
        collection.ConfigureServices(ServiceCollection);
        ServiceProvider = ServiceCollection.BuildServiceProvider();
    }
    
    protected TEntity Resolve<TEntity>() where TEntity : notnull
    {
        Debug.Assert(ServiceProvider != null, nameof(ServiceProvider) + " != null");
        return ServiceProvider.GetRequiredService<TEntity>();
    }
}

public abstract class TestBase<T> where T : class, ITestFixture, new()
{
    private ServiceProvider ServiceProvider { get; }
    private ServiceCollection ServiceCollection { get; } = new();

    protected TestBase()
    {
        var collection = new T();
        collection.ConfigureServices(ServiceCollection);
        ServiceProvider = ServiceCollection.BuildServiceProvider();
    }
    
    protected TEntity Resolve<TEntity>() where TEntity : notnull
    {
        return ServiceProvider.GetRequiredService<TEntity>();
    }
}
