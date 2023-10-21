using FluentAssertions;
using Microsoft.Playwright;
using MyTestProject.Core.Playwright;

namespace MyTestProject.Pages;

public interface IInventoryPage
{
    Task AssertIsLoaded();
    Task AddAllItemsToCart();
    IHeaderPageComponent HeaderPageComponent { get; }
}

public class InventoryPage : BasePage, IInventoryPage
{
    public IHeaderPageComponent HeaderPageComponent { get; }
    
    public InventoryPage(IHeaderPageComponent headerPageComponent, IPage page) : base(page)
    {
        HeaderPageComponent = headerPageComponent;
    }
    
    public ILocator AddBackpack => Page.Locator("#add-to-cart-sauce-labs-backpack");
    public ILocator AddBikeLight => Page.Locator("#add-to-cart-sauce-labs-bike-light");
    public ILocator AddBoltTshirt => Page.Locator("#add-to-cart-sauce-labs-bolt-t-shirt");
    public ILocator AddJacket => Page.Locator("#add-to-cart-sauce-labs-fleece-jacket");
    public ILocator AddOnesie => Page.Locator("#add-to-cart-sauce-labs-onesie");
    public ILocator AddTestAllTheThingsTshirt => Page.Locator("button:has-text('Add to cart'):below(.inventory_item_name:has-text('allTheThings')) >> nth=0");

    public async Task AssertIsLoaded()  
    {
        var titleText = await HeaderPageComponent.GetTitleText();
        titleText.Should().Be("Products");
    }

    public async Task AddAllItemsToCart()
    {
        await Page.ScreenshotAsync(new PageScreenshotOptions() { Path = "image11.jpg" });
        //await AddBackpack.ClickAsync(new LocatorClickOptions(){ClickCount = 100, Delay = 6500});
        await AddBackpack.ClickAsync();
        await AddBikeLight.ClickAsync();
        await AddBoltTshirt.ClickAsync();
        await AddJacket.ClickAsync();
        await AddOnesie.ClickAsync();
        await AddTestAllTheThingsTshirt.ClickAsync();
    }
}