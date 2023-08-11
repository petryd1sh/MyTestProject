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
        Register<JsonPlaceholderTestFixture>();
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
        Assert.That(comments, Is.Not.Empty);
    }
}