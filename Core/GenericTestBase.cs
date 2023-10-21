using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MyTestProject.Core;

public abstract class TestBase<T> where T : class, ITestFixture, new()
{
    private ServiceProvider? ServiceProvider { get; set; }
    private ServiceCollection ServiceCollection { get; } = new();
    private T TestFixture { get; } = new();
    
    protected TEntity Resolve<TEntity>() where TEntity : notnull
    {
        Console.WriteLine($"Resolving {typeof(TEntity)}");
        if (ServiceProvider == null)
        {
            CreateProvider();
        }
        return ServiceProvider.GetRequiredService<TEntity>();
    }

    private void CreateProvider()
    {
        TestFixture.ConfigureServices(ServiceCollection);
        ServiceProvider = ServiceCollection.BuildServiceProvider();
    }

    private void KillProvider()
    {
        if (ServiceProvider != null)
        {
            ServiceProvider.Dispose();
            ServiceProvider = null;
        }
    }
    // [OneTimeSetUp]
    // public void OneTimeSetup()
    // {
    //     Console.WriteLine($"OneTimeSetup");
    //     TestFixture.ConfigureServices(ServiceCollection);
    //     ServiceProvider = ServiceCollection.BuildServiceProvider();
    // }

    [SetUp]
    public void Setup()
    {
        Console.WriteLine($"Setup");
        Console.WriteLine($"{TestContext.CurrentContext.Test.FullName}");
        CreateProvider();
    }
    
    [TearDown]
    public void TearDown()
    {
        Console.WriteLine($"TearDown");
        var testName = TestContext.CurrentContext.Test.FullName;
        var result = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Success;
        var status = TestContext.CurrentContext.Result.Outcome.Status.ToString();
        Console.WriteLine($"{testName} {result} {status}");
        KillProvider();
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine($"OneTimeTearDown");
        KillProvider();
    }
}