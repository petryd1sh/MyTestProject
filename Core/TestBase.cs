using System.Configuration;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MyTestProject.Core;

public class TestBase
{
    private ServiceProvider? ServiceProvider { get; set; }
    private ServiceCollection ServiceCollection { get; } = new();

    protected void RegisterFixture<T>() where T : class, ITestFixture, new()
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
    
    [TearDown]
    public void TearDown()
    {
        var testName = TestContext.CurrentContext.Test.FullName;
        var result = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Success;
        var status = TestContext.CurrentContext.Result.Outcome.Status.ToString();
        Console.WriteLine($"{testName} {result} {status}");
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

    [TearDown]
    public void TearDown()
    {
        var testName = TestContext.CurrentContext.Test.FullName;
        var result = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Success;
        var status = TestContext.CurrentContext.Result.Outcome.Status.ToString();
        Console.WriteLine($"{testName} {result} {status}");
    }
}
