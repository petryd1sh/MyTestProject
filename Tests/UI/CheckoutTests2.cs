using FluentAssertions;
using MyTestProject.Core;
using MyTestProject.Core.Playwright;
using MyTestProject.Data;
using MyTestProject.Pages;

namespace MyTestProject.Tests.UI;

[TestFixture(typeof(SauceDemoTestFixture))]
[TestFixture(typeof(AnotherSauceDemoTestFixture))]
public class CheckoutTests2<T> : PlaywrightTestBase<T> where T : class, ITestFixture, new()
{
    public ILoginPage LoginPage;
    public IInventoryPage InventoryPage;
    public ICartPage CartPage;
    public ICheckoutPage CheckoutPage;

    [SetUp]
    public async Task Setup()
    {
        LoginPage = Resolve<ILoginPage>();
        InventoryPage = Resolve<IInventoryPage>();
        CartPage = Resolve<ICartPage>();
        CheckoutPage = Resolve<ICheckoutPage>();
        await LoginPage.LoginAs(UserLogins.StandardUser);
        await InventoryPage.AssertIsLoaded();
    }
    
    [Test]
    public async Task CanCheckout()
    {
        await InventoryPage.AddAllItemsToCart();
        await InventoryPage.HeaderPageComponent.NavigateToCart();
        await CartPage.NavigateToCheckout();
        await CheckoutPage.CompleteCheckoutForm();
        await CheckoutPage.FinishCheckout();
        await CheckoutPage.OrderComplete.WaitForVisibility();
        var orderCompleteMessage = await CheckoutPage.OrderComplete.TextContentAsync();
        orderCompleteMessage.Should().BeEquivalentTo("THANK YOU FOR YOUR ORDER!");
    }
}