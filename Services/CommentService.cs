using MyTestProject.Api;
using MyTestProject.Models;

namespace MyTestProject.Services;

public class CommentService : ICommentService
{
    private readonly ICommentsApi _commentsApi;

    public CommentService(ICommentsApi commentsApi)
    {
        _commentsApi = commentsApi;
    }
    public async Task<List<Comment>> GetComments()
    {
        return await _commentsApi.GetCommentsAsync();
    }
    public Task<Comment> GetComment(int commentId)
    {
        return Task.FromResult(_commentsApi.GetCommentAsync(commentId).Result.GetContent());
    }
}