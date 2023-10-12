﻿using MyTestProject.Api;
using MyTestProject.Models;

namespace MyTestProject.Services;

public class CommentServiceNotImplemented : ICommentService
{
    private readonly ICommentsApi _commentsApi;
    private readonly int _ms = new Random().Next(2000, 5000);
    public CommentServiceNotImplemented(ICommentsApi commentsApi)
    {
        _commentsApi = commentsApi;
    }
    public async Task<List<Comment>> GetComments()
    {
        Console.WriteLine(_ms);
        throw new NotImplementedException();
        return await _commentsApi.GetCommentsAsync();
    }
    public Task<Comment> GetComment(int commentId)
    {
        Console.WriteLine(_ms);
        throw new NotImplementedException();
        return Task.FromResult(_commentsApi.GetCommentAsync(commentId).Result.GetContent());
    }
}