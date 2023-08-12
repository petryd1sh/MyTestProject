using FluentAssertions;
using MyTestProject.Core;
using MyTestProject.Services;

namespace MyTestProject.Tests;

[Category("Comments")]
public class CommentsTests : TestBase
{
    private ICommentService CommentService => Resolve<ICommentService>();

    // public CommentsTests()
    // {
    //     Register<JsonPlaceholderTestFixture>();
    // }
    // Either ctor or setup are valid (but not both)
    [SetUp]
    public void Setup()
    {
        RegisterFixture<JsonPlaceholderTestFixture>();
    }

    [Test]
    public async Task CanGetComments()
    {
        var comments = await CommentService.GetComments();
        //Console.WriteLine(comments.Count);
        //comments.ForEach(Console.WriteLine);
        Assert.That(comments, Is.Not.Empty);
    }
}

public class CommentsTests2 : TestBase<JsonPlaceholderTestFixture>
{
    private ICommentService CommentService => Resolve<ICommentService>();

    [Test]
    public async Task CanGetComments()
    {
        var comments = await CommentService.GetComments();
        Assert.That(comments, Is.Not.Empty);
    }
}

[TestFixture(typeof(JsonPlaceholderTestFixture))]
public class CommentsTests<T> : TestBase<T> where T : class, ITestFixture, new()
{
    private ICommentService CommentService => Resolve<ICommentService>();
    [Test]
    public async Task CanGetComments()
    {
        var comments = await CommentService.GetComments();
        var commentId = 1;
        var comment1 = await CommentService.GetComment(commentId);
        Console.WriteLine(comment1);
        
        Assert.Multiple(() =>
        {
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comment1.Id, Is.EqualTo(commentId));
            
            comments.Should().NotBeEmpty();
            comment1.Id.Should().Be(1);
        });
    }
}