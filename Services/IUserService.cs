using MyTestProject.Models;

namespace MyTestProject.Services;

public interface IUserService
{
    public Task<List<User>> GetUsers();
    public Task<User> GetUser(int userId);
}