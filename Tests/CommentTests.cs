using FluentAssertions;
using MyTestProject.Core;
using MyTestProject.Services;

namespace MyTestProject.Tests;

// You can quickly change how ITestFixtures are provided to the framework for dependency injection.

// TestFixture registered in constructor or test/testfixture setup methods
[Category("Comments")]
public class CommentsTests : TestBase
{
    private ICommentService CommentService => Resolve<ICommentService>();

    // public CommentsTests()
    // {
    //     Register<JsonPlaceholderTestFixture>();
    // }
    // Either ctor or setup are valid
    [SetUp]
    public void Setup()
    {
        RegisterFixture<JsonPlaceholderTestFixture>();
    }

    [Test]
    public async Task CanGetComments()
    {
        var comments = await CommentService.GetComments();
        Assert.That(comments, Is.Not.Empty);
    }
}

// passing the TestFixture class to the TestBase
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

// generic TestFixtures passing one or many 
[TestFixture(typeof(JsonPlaceholderTestFixture))]
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