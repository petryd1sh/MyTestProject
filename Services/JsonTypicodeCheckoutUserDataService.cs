using MyTestProject.Models;

namespace MyTestProject.Services;

public class JsonTypicodeCheckoutUserDataService : ICheckoutUserDataService
{
    public IUserService UserService;
    public JsonTypicodeCheckoutUserDataService(IUserService userService)
    {
        UserService = userService;
    }
    public CheckoutUser GetCheckoutUser()
    {
        Console.WriteLine("Get JsonTypicode CheckoutUser");
        var userData = UserService.GetUser(1).GetAwaiter().GetResult();
        return new CheckoutUser()
        {
            FirstName = userData.Name.Split(' ')[0],
            LastName = userData.Name.Split(' ')[1],
            ZipCode = "11111"
        };
    }
}