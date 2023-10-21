using MyTestProject.Models;
using RestEase;

namespace MyTestProject.Api;

public interface IUsersApi
{
    [Get("/users")]
    Task<List<User>> GetUsersAsync();
    [Get("/users/{userId}")]
    Task<Response<User>> GetUserAsync([Path] int userId);
}
