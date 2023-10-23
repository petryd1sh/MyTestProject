using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Core;
using MyTestProject.Core.Playwright;
using MyTestProject.Pages;
using MyTestProject.Services;

namespace MyTestProject.Tests.UI;

public class SauceDemoTestFixture : ITestFixture
{
    public void ConfigureServices(IServiceCollection services)
    {
        this.Build<JsonPlaceholderTestFixture>(services);
        this.Build<PlaywrightTestFixture>(services);
        services.AddTransient<ILoginPage, LoginPage>();
        services.AddTransient<IHeaderPageComponent, HeaderPageComponent>();
        services.AddTransient<IInventoryPage, InventoryPage>();
        services.AddTransient<IItemPage, ItemPage>();
        services.AddTransient<ICartPage, CartPage>();
        services.AddTransient<ICheckoutPage, CheckoutPage>();
        services.AddTransient<ICheckoutUserDataService, JsonTypicodeCheckoutUserDataService>();
    }
}

public class AnotherSauceDemoTestFixture : ITestFixture
{
    public void ConfigureServices(IServiceCollection services)
    {
        this.Build<SauceDemoTestFixture>(services);
        services.AddTransient<ICheckoutUserDataService, FakerCheckoutUserDataService>();
    }
}