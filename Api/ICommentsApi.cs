using MyTestProject.Models;
using RestEase;

namespace MyTestProject.Api;

public interface ICommentsApi
{
    [Get("/comments")]
    Task<List<Comment>> GetCommentsAsync();

    [Post("/comments")]
    Task<Response<Comment>> PostCommentAsync([Body] Comment comment);
}
