using System.Net;

namespace MyTestProject.Core.RestEase;

public class SomeHandler : DelegatingHandler
{
    // TODO should work like restassured filters for logging request/responses
    // protected override Task<HttpResponseMessage> SendAsync(
    //     HttpRequestMessage request, CancellationToken cancellationToken)
    // {
    //     // Create the response.
    //     var response = new HttpResponseMessage(HttpStatusCode.OK)
    //     {
    //         Content = new StringContent("Hello!")
    //     };
    //     
    //     // Note: TaskCompletionSource creates a task that does not contain a delegate.
    //     var tsc = new TaskCompletionSource<HttpResponseMessage>();
    //     tsc.SetResult(response);   // Also sets the task state to "RanToCompletion"
    //    return tsc.Task;
    // }
}