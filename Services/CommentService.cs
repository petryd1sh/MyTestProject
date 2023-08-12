using MyTestProject.Api;
using MyTestProject.Models;

namespace MyTestProject.Services;

public class CommentService : ICommentService
{
    private readonly ICommentsApi _commentsApi;
    private readonly int _ms = new Random().Next(2000, 5000);
    public CommentService(ICommentsApi commentsApi)
    {
        _commentsApi = commentsApi;
    }
    public async Task<List<Comment>> GetComments()
    {
        Console.WriteLine(_ms);
        return await _commentsApi.GetCommentsAsync();
    }
    public Task<Comment> GetComment(int commentId)
    {
        Console.WriteLine(_ms);
        return Task.FromResult(_commentsApi.GetCommentAsync(commentId).Result.GetContent());
    }
}