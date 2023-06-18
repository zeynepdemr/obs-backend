using Obs.Common.Infrastructure.Installers;
using Obs.Domain.Options;

namespace Obs.API.Installers;

public class OptionsInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.Configure<SwaggerOptions>(configuration.GetSection(nameof(SwaggerOptions)));
    }
}