using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Obs.Application.Services.StudentService;
using Obs.Common.Infrastructure.Installers;
using Obs.Persistence.Services;

namespace Obs.Persistence.Installers;

public class ServiceInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStudentService, StudentService>();
    }
}