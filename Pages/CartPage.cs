using FluentAssertions;
using Microsoft.Playwright;
using MyTestProject.Core.Playwright;

namespace MyTestProject.Pages;

public interface ICartPage
{
    Task AssertIsLoaded();
    Task AssertAllItems();
    Task RemoveAllItems();
    Task AssertNoItems();
    Task NavigateToCheckout();
}

public class CartPage : BasePage, ICartPage
{
    public IHeaderPageComponent HeaderPageComponent;
    public CartPage(IHeaderPageComponent headerPageComponent, IPage page) : base(page)
    {
        HeaderPageComponent = headerPageComponent;
    }
    
    public ILocator RemoveBackpack => Page.Locator("#remove-sauce-labs-backpack");
    public ILocator RemoveBikeLight => Page.Locator("#remove-sauce-labs-bike-light");
    public ILocator RemoveBoltTshirt => Page.Locator("#remove-sauce-labs-bolt-t-shirt");
    public ILocator RemoveJacket => Page.Locator("#remove-sauce-labs-fleece-jacket");
    public ILocator RemoveOnesie => Page.Locator("#remove-sauce-labs-onesie");
    //public ILocator RemoveTestAllTheThingsTshirt => Page.Locator("button:has-text('Remove'):near(.inventory_item_name:has-text('allTheThings'))");
    public ILocator RemoveTestAllTheThingsTshirt => Page.Locator("button:has-text('Remove'):near(:text('allTheThings'), 80)");
    public ILocator RemoveTestAllTheThingsTshirtLink => Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Test.allTheThings() T-Shirt (Red)" });
    public ILocator CartItem => Page.Locator(".cart_item");
    public ILocator Checkout => Page.Locator("#checkout");

    public async Task AssertIsLoaded()
    {
        var titleText = await HeaderPageComponent.GetTitleText();
        titleText.Should().Be("Your Cart");
    }

    public async Task AssertAllItems()
    {
        await AssertIsLoaded();
        var removeBackpackVisible = await RemoveBackpack.IsVisibleAsync();
        var removeBikeLightVisible = await RemoveBikeLight.IsVisibleAsync();
        var removeBoltTshirtVisible = await RemoveBoltTshirt.IsVisibleAsync();
        var removeJacketVisible = await RemoveJacket.IsVisibleAsync();
        var removeOnesieVisible = await RemoveOnesie.IsVisibleAsync();
        var removeTestAllTheThingsTshirtVisible = false;// await RemoveTestAllTheThingsTshirt.IsVisibleAsync();

        var cartItems = await CartItem.ElementHandlesAsync();
        foreach (var item in cartItems)
        {
            Console.WriteLine(item.TextContentAsync().Result);
            removeTestAllTheThingsTshirtVisible = item.TextContentAsync().Result.Contains("allTheThings");
        }
        var cartItemCount = cartItems.Count;
        
        Assert.Multiple(() =>
        {
            removeBackpackVisible.Should().BeTrue();
            removeBikeLightVisible.Should().BeTrue();
            removeBoltTshirtVisible.Should().BeTrue();
            removeJacketVisible.Should().BeTrue();
            removeOnesieVisible.Should().BeTrue();
            removeTestAllTheThingsTshirtVisible.Should().BeTrue();
            cartItemCount.Should().Be(6);
        });
    }

    public async Task RemoveAllItems()
    {
        await AssertIsLoaded();
        await RemoveBackpack.ClickAsync();
        await RemoveBikeLight.ClickAsync();
        await RemoveBoltTshirt.ClickAsync();
        await RemoveJacket.ClickAsync();
        await RemoveOnesie.ClickAsync();
        await RemoveTestAllTheThingsTshirt.ClickAsync();
    }

    public async Task AssertNoItems()
    {
        await AssertIsLoaded();
        var cartItems = await CartItem.ElementHandlesAsync();
        var cartItemCount = cartItems.Count;
        cartItemCount.Should().Be(0);
    }

    public async Task NavigateToCheckout()
    {
        await AssertIsLoaded();
        await Checkout.ClickAsync();
    }
}