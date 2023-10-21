using Microsoft.Playwright;
using MyTestProject.Core.Playwright;

namespace MyTestProject.Pages;

public interface IHeaderPageComponent 
{
    public Task<string> GetTitleText();
    public Task NavigateToCart();
}

public class HeaderPageComponent : BasePage, IHeaderPageComponent
{
    public HeaderPageComponent(IPage page) : base(page)
    {
    }
    public ILocator Title => Page.Locator(".title");
    public ILocator Cart => Page.Locator(".shopping_cart_link");
    
    public async Task<string> GetTitleText()
    {
        await Title.WaitForVisibility();
        return await Title.TextContentAsync();
    }

    public async Task NavigateToCart()
    {
        await Cart.ClickAsync();
    }
}