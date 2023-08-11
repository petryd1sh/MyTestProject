using MyTestProject.Models;
using RestEase;

namespace MyTestProject.Api;

public interface ICommentsApi
{
    [Get("/comments")]
    Task<List<Comment>> GetCommentsAsync();
    [Get("/comments/{commentId}")]
    Task<Response<Comment>> GetCommentAsync([Path] int commentId);
    [Post("/comments")]
    Task<Response<Comment>> PostCommentAsync([Body] Comment comment);
}
