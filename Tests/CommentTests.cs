using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Core;
using MyTestProject.Services;

namespace MyTestProject.Tests;

public class CommentsTests : TestBase
{
    private ICommentService CommentService => Resolve<ICommentService>();
    
    [SetUp]
    public void Setup()
    {
        Register<JsonPlaceholderTestFixture>();
    }

    [Test]
    public async Task Test1()
    {
        var comments = await CommentService.GetComments();
        Console.WriteLine(comments.Count);
        comments.ForEach(Console.WriteLine);
        Assert.That(comments, Is.Not.Empty);
        Assert.That(comments, Has.Count.EqualTo(500));
    }
}

[TestFixture(typeof(JsonPlaceholderTestFixture))]
public class CommentsTests<T> : TestBase<T> where T : class, IConfigureServices, new()
{
    private ICommentService CommentService => Resolve<ICommentService>();
    [Test]
    public async Task Test1()
    {
        var comments = await CommentService.GetComments();
        Console.WriteLine(comments.Count);
        comments.ForEach(Console.WriteLine);
        Assert.That(comments, Is.Not.Empty);
        Assert.That(comments, Has.Count.EqualTo(500));
    }
}

public class MyApplicationBaseTest : TestBase<JsonPlaceholderTestFixture>
{
    public ICommentService CommentService;

    [SetUp]
    public void Setup()
    {
        CommentService = Resolve<ICommentService>();
    }
    
    [Test]
    public async Task Test1()
    {
        var comments = await CommentService.GetComments();
        Console.WriteLine(comments.Count);
        comments.ForEach(Console.WriteLine);
        Assert.That(comments, Is.Not.Empty);
        Assert.That(comments, Has.Count.EqualTo(500));
    }
}

public class MyApplicationBaseTest2 : TestBase<JsonPlaceholderTestFixture>
{
    public ICommentService CommentService; // show with generated constructor and why it does not work (parameterless ctors only)

    public MyApplicationBaseTest2(ICommentService commentService)
    {
        CommentService = commentService;
    }

    [SetUp]
    public void Setup()
    {
        //CommentService = Resolve<ICommentService>();
    }
    //[Test]
    public async Task Test1()
    {
        var comments = await CommentService.GetComments();
        Console.WriteLine(comments.Count);
        comments.ForEach(Console.WriteLine);
        Assert.That(comments, Is.Not.Empty);
        Assert.That(comments, Has.Count.EqualTo(500));
    }
}