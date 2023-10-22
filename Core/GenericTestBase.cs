using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace MyTestProject.Core;

public abstract class TestBase<T> where T : class, ITestFixture, new()
{
    private ServiceProvider? ServiceProvider { get; set; }
    private ServiceCollection ServiceCollection { get; } = new();
    private T TestFixture { get; } = new();
    
    private void RegisterTestFixture()
    {
        try
        {
            Console.WriteLine($"RegisterTestFixture {typeof(T)}");
            TestFixture.ConfigureServices(ServiceCollection);
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void TearDownTestFixture()
    {
        if (ServiceProvider != null)
        {
            try
            {
                Console.WriteLine($"TearDownTestFixture {typeof(T)}");
                ServiceProvider.Dispose();
                ServiceProvider = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
    
    protected TEntity Resolve<TEntity>() where TEntity : notnull
    {
        Console.WriteLine($"Resolving {typeof(TEntity)}");
        if (ServiceProvider == null)
        {
            RegisterTestFixture();
        }
        return ServiceProvider.GetRequiredService<TEntity>();
    }
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Console.WriteLine($"OneTimeSetup");
        RegisterTestFixture();
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
        TearDownTestFixture();
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine($"OneTimeTearDown");
        TearDownTestFixture();
    }
}