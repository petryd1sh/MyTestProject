using MyTestProject.Api;
using MyTestProject.Models;
using MyTestProject.Tests;
using RestEase;

namespace MyTestProject.Services;

public class CommentService : ICommentService
{
    private ICommentsApi _commentsApi;

    public CommentService(ICommentsApi commentsApi)
    {
        _commentsApi = commentsApi;
    }
    public async Task<List<Comment>> GetComments()
    {
        return await _commentsApi.GetCommentsAsync();
    }
}