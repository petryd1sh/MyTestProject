using FluentAssertions;
using MyTestProject.Core.Playwright;
using MyTestProject.Data;
using MyTestProject.Pages;

namespace MyTestProject.Tests.UI;

public class LoginTests : PlaywrightTestBase<SauceDemoTestFixture>
{
    public ILoginPage LoginPage;
    public IInventoryPage InventoryPage;

    [SetUp]
    public void Setup()
    {
        LoginPage = Resolve<ILoginPage>();
        InventoryPage = Resolve<IInventoryPage>();
    }

    [Test]
    public async Task CanLogin()
    {
        await LoginPage.LoginAs(UserLogins.StandardUser);
        var errorMessageVisible = await LoginPage.ErrorMessage.IsVisibleAsync();
        errorMessageVisible.Should().BeFalse("expect login success");
        await InventoryPage.AssertIsLoaded();
    }

    [Test]
    public async Task LockedOutUserReceivesLockedOutErrorMessage()
    {
        await LoginPage.LoginAs(UserLogins.LockedOutUser);
        var errorMessageVisible = await LoginPage.ErrorMessage.IsVisibleAsync();
        errorMessageVisible.Should().BeTrue("expect login error message");
        var errorMessageText = await LoginPage.ErrorMessage.TextContentAsync();
        errorMessageText.Should().Be("Epic sadface: Sorry, this user has been locked out.");
    }
    
    [Test]
    public async Task CanGetInvalidUsernamePasswordErrorMessage()
    {
        await LoginPage.LoginAs(UserLogins.StandardUserBadPassword);
        var errorMessageVisible = await LoginPage.ErrorMessage.IsVisibleAsync();
        errorMessageVisible.Should().BeTrue("expect login error message");
        var errorMessageText = await LoginPage.ErrorMessage.TextContentAsync();
        errorMessageText.Should().Be("Epic sadface: Username and password do not match any user in this service");
    }
}