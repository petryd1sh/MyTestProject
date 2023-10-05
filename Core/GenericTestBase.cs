using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MyTestProject.Core;

public abstract class TestBase<T> where T : class, ITestFixture, new()
{
    private ServiceProvider ServiceProvider { get; set; } = null!;
    private ServiceCollection ServiceCollection { get; } = new();
    private T TestFixture { get; } = new();
    
    protected TEntity Resolve<TEntity>() where TEntity : notnull
    {
        Console.WriteLine($"Resolving {typeof(TEntity)}");
        return ServiceProvider.GetRequiredService<TEntity>();
    }
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Console.WriteLine($"OneTimeSetup");
        TestFixture.ConfigureServices(ServiceCollection);
        ServiceProvider = ServiceCollection.BuildServiceProvider();
    }

    [SetUp]
    public void Setup()
    {
        Console.WriteLine($"Setup");
        Console.WriteLine($"{TestContext.CurrentContext.Test.FullName}");
    }
    
    [TearDown]
    public void TearDown()
    {
        Console.WriteLine($"TearDown");
        var testName = TestContext.CurrentContext.Test.FullName;
        var result = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Success;
        var status = TestContext.CurrentContext.Result.Outcome.Status.ToString();
        Console.WriteLine($"{testName} {result} {status}");
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine($"OneTimeTearDown");
        ServiceProvider.Dispose();
    }
}