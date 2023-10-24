using Microsoft.Playwright;
using MyTestProject.Core.Config;

namespace MyTestProject.Core.Playwright;

[Parallelizable]
public class PlaywrightTestBase<TFixture> : TestBase<TFixture> where TFixture : class, ITestFixture, new()
{
    private IPlaywright Playwright;
    private IBrowser Browser;
    private IPage Page;

    [SetUp]
    public async Task WebTestSetup()
    {
        Playwright = Resolve<IPlaywright>();
        Browser = Resolve<IBrowser>();
        Page = Resolve<IPage>();
        
        var baseUrl = TestConfig.GetTestRunParameter("baseUrl");
        await TestContext.Out.WriteLineAsync($"{nameof(baseUrl)} {baseUrl}");
        await Page.GotoAsync(baseUrl).ConfigureAwait(false);
    }

    [TearDown]
    public async Task WebTestTearDown()
    {
        var result = TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Success;
        if (!result)
        {
            try
            {
                var screenshot = await Page.ScreenshotAsync().ConfigureAwait(false);
                var filename = $"{TestContext.CurrentContext.Test.Name}{DateTime.Now:dd-MM-yyyy-hhmm-ss}.jpg";
                var screenshotFile = Path.Combine(TestContext.CurrentContext.WorkDirectory, filename);
                await File.WriteAllBytesAsync(screenshotFile, screenshot);
                TestContext.AddTestAttachment(screenshotFile, "Failure screenshot");
            }
            catch
            {
                Console.WriteLine("Failed to take screenshot.");
            }
        }
    }

    [OneTimeTearDown]
    public async Task PlaywrightOneTimeTearDown()
    {
        await Page.CloseAsync().ConfigureAwait(false);
        await Browser.CloseAsync().ConfigureAwait(false);
        Playwright.Dispose();
    }
}