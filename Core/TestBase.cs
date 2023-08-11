using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyTestProject.Core;

public class TestBase
{
    public ServiceProvider ServiceProvider { get; set; }
    public ServiceCollection ServiceCollection { get; } = new();

    protected void Register<T>() where T : class, IConfigureServices, new()
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



public abstract class TestBase<T> where T : class, IConfigureServices, new() // some interface with new constraint
{
    public ServiceProvider ServiceProvider { get; set; }
    public ServiceCollection ServiceCollection { get; } = new();
    public TestBase()
    {
        Register<T>();
    }
    protected void Register<T>() where T : class, IConfigureServices, new()
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