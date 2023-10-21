using Microsoft.Playwright;

namespace MyTestProject.Core.Playwright;

public static class PlaywrightFactory
{
    public static async Task<IPlaywright> GetPlaywright()
    {
        return await Microsoft.Playwright.Playwright.CreateAsync();
    }

    public static async Task<IBrowser> GetBrowser(IPlaywright playwright)
    {
        var runsettingsTimeoutInSeconds = Convert.ToInt32(TestContext.Parameters["timeoutInSeconds"]);
        var timeout = (runsettingsTimeoutInSeconds == 0 ? 10 : runsettingsTimeoutInSeconds) * 1000;
        var headless = Convert.ToBoolean(TestContext.Parameters["headless"] ?? "true");
        // TODO support other browsers via .runsettings
        return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Timeout = timeout, Headless = headless });
    }

    public static async Task<IPage> GetPage(IBrowser browser)
    {
        return await browser.NewPageAsync();
    }
}