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
    private ICommentService CommentService;

    [SetUp]
    public void Setup()
    {
        CommentService = Resolve<ICommentService>();
    }
    
    [Test]
    public async Task CanGetComments()
    {
        var comments = await CommentService.GetComments();
        Assert.That(comments, Is.Not.Empty);
    }
}

// generic TestFixtures passing one or many 
[Parallelizable(ParallelScope.Fixtures)]
[TestFixture(typeof(JsonPlaceholderTestFixture))]
//[TestFixture(typeof(AnotherJsonPlaceholderTestFixture))]
//[TestFixtureSource(typeof(MyTestFixtures),nameof(MyTestFixtures.FixtureServicesList))]
public class CommentsTests<T> : TestBase<T> where T : class, ITestFixture, new()
{
    private ICommentService CommentService { get; set; }

    [OneTimeSetUp]
    //[SetUp]
    public void Setup()
    {
        CommentService = Resolve<ICommentService>();
    }
    
    [Test]
    public async Task CanGetComments()
    {
        var comments = await CommentService.GetComments();
        var commentId = 1;
        var comment1 = await CommentService.GetComment(commentId);
        Console.WriteLine(comment1);
        var ms = new Random().Next(2000, 5000);
        Thread.Sleep(ms);
        Assert.Multiple(() =>
        {
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comment1.Id, Is.EqualTo(commentId));
            
            comments.Should().NotBeEmpty();
            comment1.Id.Should().Be(1);
        });
    }
    [Test]
    public async Task CanGetComments2()
    {
        var comments = await CommentService.GetComments();
        var commentId = 1;
        var comment1 = await CommentService.GetComment(commentId);
        Console.WriteLine(comment1);
        var ms = new Random().Next(2000, 5000);
        Thread.Sleep(ms);
        Assert.Multiple(() =>
        {
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comment1.Id, Is.EqualTo(commentId));
            
            comments.Should().NotBeEmpty();
            comment1.Id.Should().Be(1);
        });
    }
    [Test]
    public async Task CanGetComments3()
    {
        var comments = await CommentService.GetComments();
        var commentId = 1;
        var comment1 = await CommentService.GetComment(commentId);
        Console.WriteLine(comment1);
        var ms = new Random().Next(2000, 5000);
        Thread.Sleep(ms);
        Assert.Multiple(() =>
        {
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comment1.Id, Is.EqualTo(commentId));
            
            comments.Should().NotBeEmpty();
            comment1.Id.Should().Be(1);
        });
    }
}



[Parallelizable(ParallelScope.Fixtures)]
[TestFixture(typeof(JsonPlaceholderTestFixture))]
public class CommentsTests3<T> : TestBase2<T> where T : class, ITestFixture, new()
{
    private ICommentService CommentService { get; set; }

    [OneTimeSetUp]
    //[SetUp]
    public void Setup()
    {
        CommentService = Resolve<ICommentService>();
    }
    
    [Test]
    public async Task CanGetComments()
    {
        var comments = await CommentService.GetComments();
        var commentId = 1;
        var comment1 = await CommentService.GetComment(commentId);
        Console.WriteLine(comment1);
        var ms = new Random().Next(2000, 5000);
        Thread.Sleep(ms);
        Assert.Multiple(() =>
        {
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comment1.Id, Is.EqualTo(commentId));
            
            comments.Should().NotBeEmpty();
            comment1.Id.Should().Be(1);
        });
    }
    [Test]
    public async Task CanGetComments2()
    {
        var comments = await CommentService.GetComments();
        var commentId = 1;
        var comment1 = await CommentService.GetComment(commentId);
        Console.WriteLine(comment1);
        var ms = new Random().Next(2000, 5000);
        Thread.Sleep(ms);
        Assert.Multiple(() =>
        {
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comment1.Id, Is.EqualTo(commentId));
            
            comments.Should().NotBeEmpty();
            comment1.Id.Should().Be(1);
        });
    }
    [Test]
    public async Task CanGetComments3()
    {
        var comments = await CommentService.GetComments();
        var commentId = 1;
        var comment1 = await CommentService.GetComment(commentId);
        Console.WriteLine(comment1);
        var ms = new Random().Next(2000, 5000);
        Thread.Sleep(ms);
        Assert.Multiple(() =>
        {
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comment1.Id, Is.EqualTo(commentId));
            
            comments.Should().NotBeEmpty();
            comment1.Id.Should().Be(1);
        });
    }
}