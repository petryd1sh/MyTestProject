using MyTestProject.Models;

namespace MyTestProject.Services;

public class JsonTypicodeCheckoutUserDataService : ICheckoutUserDataService
{
    private readonly IUserService _userService;
    public JsonTypicodeCheckoutUserDataService(IUserService userService)
    {
        _userService = userService;
    }
    public CheckoutUser GetCheckoutUser()
    {
        Console.WriteLine("Get JsonTypicode CheckoutUser");
        var userData = _userService.GetUser(1).GetAwaiter().GetResult();
        return new CheckoutUser()
        {
            FirstName = userData.Name?.Split(' ')[0],
            LastName = userData.Name?.Split(' ')[1],
            ZipCode = "11111"
        };
    }
}