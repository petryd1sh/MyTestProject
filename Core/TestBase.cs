using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MyTestProject.Core;

public class TestBase
{
    private ServiceProvider ServiceProvider { get; set; } = null!;
    private ServiceCollection ServiceCollection { get; } = new();

    protected void RegisterFixture<T>() where T : class, ITestFixture, new()
    {
        Console.WriteLine($"Register Test Fixture {typeof(T)}");
        var collection = new T();
        collection.ConfigureServices(ServiceCollection);
        ServiceProvider = ServiceCollection.BuildServiceProvider();
    }
    
    protected TEntity Resolve<TEntity>() where TEntity : notnull
    {
        Console.WriteLine($"Resolving {typeof(TEntity)}");
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
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        try
        {
            Console.WriteLine($"TearDown Test Fixture");
            ServiceProvider.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}