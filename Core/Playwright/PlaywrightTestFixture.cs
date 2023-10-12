using Microsoft.Extensions.DependencyInjection;

namespace MyTestProject.Core.Playwright;

public class PlaywrightTestFixture : ITestFixture
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        var playwright = PlaywrightFactory.GetPlaywright().GetAwaiter().GetResult();
        var browser = PlaywrightFactory.GetBrowser(playwright).GetAwaiter().GetResult();
        var page = PlaywrightFactory.GetPage(browser).GetAwaiter().GetResult();
        serviceCollection.AddSingleton(playwright);
        serviceCollection.AddSingleton(browser);
        serviceCollection.AddSingleton(page);
    }
}