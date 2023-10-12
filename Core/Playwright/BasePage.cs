using Microsoft.Playwright;

namespace MyTestProject.Core.Playwright;

public abstract class BasePage
{
    protected IPage Page;

    protected BasePage(IPage page)
    {
        Page = page;
    }
}