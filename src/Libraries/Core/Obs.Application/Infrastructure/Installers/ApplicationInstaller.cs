using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Obs.Common.Infrastructure.Installers;

namespace Obs.Application.Infrastructure.Installers;

public class ApplicationInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyMarker>();
        });
    }
}