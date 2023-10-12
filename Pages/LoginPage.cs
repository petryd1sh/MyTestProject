using Microsoft.Playwright;
using MyTestProject.Core.Playwright;
using MyTestProject.Models;

namespace MyTestProject.Pages;

public interface ILoginPage
{
    Task LoginAs(UserLogin username);
    ILocator ErrorMessage { get; }
}

public class LoginPage : BasePage, ILoginPage
{
    public LoginPage(IPage page) : base(page)
    {
    }

    public ILocator UserName => Page.Locator("#user-name");
    public ILocator Password => Page.Locator("#password");
    public ILocator LoginButton => Page.Locator("#login-button");
    public ILocator ErrorMessage => Page.Locator(".error-message-container");
    
    public async Task LoginAs(UserLogin userLogin)
    {
        await UserName.WaitForVisibility();
        await UserName.FillAsync(userLogin.UserName);
        await Password.FillAsync(userLogin.Password);
        await LoginButton.ClickAsync();
    }
}