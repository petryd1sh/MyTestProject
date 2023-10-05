using FluentAssertions;
using MyTestProject.Core;
using MyTestProject.Services;

namespace MyTestProject.Tests;

// You can quickly change how ITestFixtures are provided to the framework for dependency injection.

// TestFixture registered in constructor or test/testfixture setup methods
[Category("Comments")]
public class CommentsTests : TestBase
{
    private ICommentService CommentService; //=> Resolve<ICommentService>(); // this causes the object to be recreated for each new method call
    
    [SetUp]
    public void Setup()
    {
        RegisterFixture<JsonPlaceholderTestFixture>();
        CommentService = Resolve<ICommentService>();
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

    [SetUp]
    public void Setup()
    {
        //CommentService = Resolve<ICommentService>();
    }
    
    [Test]
    public async Task CanGetComments()
    {
        var comments = await CommentService.GetComments();
        Assert.That(comments, Is.Not.Empty);
    }
}