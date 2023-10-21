using MyTestProject.Api;
using MyTestProject.Models;

namespace MyTestProject.Services;

public class UserService : IUserService
{
    private readonly IUsersApi _usersApi;
    private readonly int _ms = new Random().Next(2000, 5000);
    public UserService(IUsersApi usersApi)
    {
        _usersApi = usersApi;
    }

    public async Task<List<User>> GetUsers()
    {
        Console.WriteLine(_ms);
        return await _usersApi.GetUsersAsync();
    }

    public Task<User> GetUser(int userId)
    {
        Console.WriteLine(_ms);
        return Task.FromResult(_usersApi.GetUserAsync(userId).Result.GetContent());
    }
}