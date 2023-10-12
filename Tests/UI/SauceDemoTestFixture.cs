using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Core;
using MyTestProject.Pages;

namespace MyTestProject.Tests.UI;

public class SauceDemoTestFixture : ITestFixture
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<ILoginPage, LoginPage>();
        services.AddTransient<IHeaderPageComponent, HeaderPageComponent>();
        services.AddTransient<IInventoryPage, InventoryPage>();
        services.AddTransient<IItemPage, ItemPage>();
        services.AddTransient<ICartPage, CartPage>();
        services.AddTransient<ICheckoutPage, CheckoutPage>();
    }
}