using Microsoft.Playwright;
using MyTestProject.Core.Playwright;

namespace MyTestProject.Pages;

public interface IItemPage
{
    
}

public class ItemPage : BasePage, IItemPage
{
    public IHeaderPageComponent HeaderPageComponent;
    public ItemPage(IHeaderPageComponent headerPageComponent, IPage page) : base(page)
    {
        HeaderPageComponent = headerPageComponent;
    }
}