namespace MyTestProject.Core.RestEase;

public class LoggingHandler : DelegatingHandler
{
    private readonly bool _outputResponse;
    public LoggingHandler(bool outputResponse = false)
    {
        _outputResponse = outputResponse;
    }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {   
        //Console.WriteLine(request.ToString());
        Console.WriteLine($"{request.RequestUri} {request.Method}");
        // if (!request.Headers.Contains("Authorization"))
        // {
        //     Console.WriteLine("No Authorization Header found");
        // }
        var response = base.SendAsync(request, cancellationToken);
        Console.WriteLine(response.GetAwaiter().GetResult().StatusCode);
        if (_outputResponse)
        {
            //Console.WriteLine(response.GetAwaiter().GetResult().ToString());
            Console.WriteLine(response.GetAwaiter().GetResult().Content.ToString());
        }
        return response;
    }
}