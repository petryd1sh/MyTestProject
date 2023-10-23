using Bogus;
using MyTestProject.Models;

namespace MyTestProject.Services;

public class FakerCheckoutUserDataService : ICheckoutUserDataService
{
    public CheckoutUser GetCheckoutUser()
    {
        Console.WriteLine("Get Faker CheckoutUser");
        var userData = new Faker().Person;
        return new CheckoutUser()
         {
            FirstName = userData.FirstName,
            LastName = userData.LastName,
            ZipCode = userData.Address.ZipCode
         };
    }
}