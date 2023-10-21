using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MyTestProject.Core;
using MyTestProject.Services;

namespace MyTestProject.Tests;

// generic TestFixtures passing one or many 
[Parallelizable(ParallelScope.Fixtures)]
[TestFixture(typeof(JsonPlaceholderTestFixture))]
[TestFixture(typeof(JsonPlaceholderTestFixture))]
//[TestFixture(typeof(AnotherJsonPlaceholderTestFixture))]
//[TestFixtureSource(typeof(MyTestFixtures),nameof(MyTestFixtures.FixtureServicesList))]
public class GenericFixturesCommentsTests<T> : TestBase<T> where T : class, ITestFixture, new()
{
    private ICommentService CommentService;// => Resolve<ICommentService>(); // creates an instance every reference
   // private ICommentService _commentService2;
    
    private const int CommentId = 1;

    [OneTimeSetUp]
    public void CommentTestsOneTimeSetUp()
    {
        //CommentService = Resolve<ICommentService>();
    }
    
    [SetUp]
    public void CommentTestsSetup()
    {
        CommentService = Resolve<ICommentService>();
    }
    
    [Test]
    public async Task CanGetComments()
    {
        var comments = await CommentService.GetComments();
        //var comments2 = await _commentService2.GetComments();
        var comment1 = await CommentService.GetComment(CommentId);
        Console.WriteLine(comments.Count);
        Console.WriteLine(comment1);
        var ms = new Random().Next(2000, 5000);
        Thread.Sleep(ms); // This is only for showing parallel test execution.
        Assert.Multiple(() =>
        {
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comment1.Id, Is.EqualTo(CommentId));
            
            comments.Should().NotBeEmpty();
            comment1.Id.Should().Be(CommentId);
        });
    }
    
    [Test]
    public async Task CanGetComments2()
    {
        var comments = await CommentService.GetComments();
        var comment1 = await CommentService.GetComment(CommentId);
        Console.WriteLine(comments.Count);
        Console.WriteLine(comment1);
        var ms = new Random().Next(2000, 5000);
        Thread.Sleep(ms); // This is only for showing parallel test execution.
        Assert.Multiple(() =>
        {
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comment1.Id, Is.EqualTo(CommentId));
            
            comments.Should().NotBeEmpty();
            comment1.Id.Should().Be(CommentId);
        });
    }
    
    [Test]
    public async Task CanGetComments3()
    {
        var comments = await CommentService.GetComments();
        var comment1 = await CommentService.GetComment(CommentId);
        Console.WriteLine(comments.Count);
        Console.WriteLine(comment1);
        var ms = new Random().Next(2000, 5000);
        Thread.Sleep(ms); // This is only for showing parallel test execution.
        Assert.Multiple(() =>
        {
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comment1.Id, Is.EqualTo(CommentId));
            
            comments.Should().NotBeEmpty();
            comment1.Id.Should().Be(CommentId);
        });
    }
}