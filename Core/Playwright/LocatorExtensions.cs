using Microsoft.Playwright;

namespace MyTestProject.Core.Playwright;

public static class LocatorExtensions
{
    public static async Task WaitForVisibility(this ILocator locator)
    {
        await locator.WaitForAsync(new LocatorWaitForOptions()
        {
            State = WaitForSelectorState.Visible
        });
    }
    public static async Task WaitForHidden(this ILocator locator)
    {
        await locator.WaitForAsync(new LocatorWaitForOptions()
        {
            State = WaitForSelectorState.Hidden
        });
    }
}