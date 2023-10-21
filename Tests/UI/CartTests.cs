using MyTestProject.Core.Playwright;
using MyTestProject.Data;
using MyTestProject.Pages;

namespace MyTestProject.Tests.UI;

public class CartTests : PlaywrightTestBase<SauceDemoTestFixture>
{
    public ILoginPage LoginPage;
    public IInventoryPage InventoryPage;
    public ICartPage CartPage;

    [SetUp]
    public async Task Setup()
    {
        //Register<SauceDemoTestFixture>();
        LoginPage = Resolve<ILoginPage>();
        InventoryPage = Resolve<IInventoryPage>();
        CartPage = Resolve<ICartPage>();
        await LoginPage.LoginAs(UserLogins.StandardUser);
        await InventoryPage.AssertIsLoaded();
    }

    [Test]
    public async Task CanAddAllItemsToCart()
    {
        await InventoryPage.AddAllItemsToCart();
        await InventoryPage.HeaderPageComponent.NavigateToCart();
        await CartPage.AssertAllItems();
    }
    
    [Test]
    public async Task CanRemoveAllItemsFromCart()
    {
        await InventoryPage.AddAllItemsToCart();
        await InventoryPage.HeaderPageComponent.NavigateToCart();
        await CartPage.RemoveAllItems();
        await CartPage.AssertNoItems();
    }
}