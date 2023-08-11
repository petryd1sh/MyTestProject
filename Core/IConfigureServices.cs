using Microsoft.Extensions.DependencyInjection;

namespace MyTestProject.Core;

public interface IConfigureServices
{
    public void ConfigureServices(IServiceCollection services);
}