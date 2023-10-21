using Bogus;
using FluentAssertions;
using Microsoft.Playwright;
using MyTestProject.Api;
using MyTestProject.Core.Playwright;
using MyTestProject.Services;

namespace MyTestProject.Pages;

public interface ICheckoutPage
{
    Task CompleteCheckoutForm();
    Task FinishCheckout();
    ILocator OrderComplete { get; }
}

public class CheckoutPage : BasePage, ICheckoutPage
{
    public IHeaderPageComponent HeaderPageComponent;
    public IUserService UserService;

    public CheckoutPage(IUserService userService, IHeaderPageComponent headerPageComponent, IPage page) : base(page)
    {
        UserService = userService;
        HeaderPageComponent = headerPageComponent;
    }

    public ILocator FirstName => Page.Locator("#first-name");
    public ILocator LastName => Page.Locator("#last-name");
    public ILocator PostalCode => Page.Locator("#postal-code");
    public ILocator Continue => Page.Locator("#continue");
    public ILocator Finish => Page.Locator("#finish");
    public ILocator OrderComplete => Page.Locator(".complete-header");
    
    public async Task AssertIsLoaded(string text)
    {
        var titleText = await HeaderPageComponent.GetTitleText();
        titleText.Should().Be(text);
    }

    public async Task CompleteCheckoutForm()
    {
        await AssertIsLoaded("Checkout: Your Information");
        //var userData = new Faker().Person;
        // await FirstName.FillAsync(userData.FirstName);
        // await LastName.FillAsync(userData.LastName);
        // await PostalCode.FillAsync(userData.Address.ZipCode);
        var userData = await UserService.GetUser(1);
        await FirstName.FillAsync(userData.Name.Split(' ')[0]);
        await LastName.FillAsync(userData.Name.Split(' ')[1]);
        await PostalCode.FillAsync("11111");
        await Continue.ClickAsync();
    }

    public async Task FinishCheckout()
    {
        await AssertIsLoaded("Checkout: Overview");
        await Finish.ClickAsync();
    }
}