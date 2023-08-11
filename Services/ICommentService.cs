using MyTestProject.Models;

namespace MyTestProject.Services;

public interface ICommentService
{
    public Task<List<Comment>> GetComments();
    public Task<Comment> GetComment(int commentId);
}