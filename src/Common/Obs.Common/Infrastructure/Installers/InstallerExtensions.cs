using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Obs.Common.Infrastructure.Installers;

public static class InstallerExtensions
{
    public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration, List<Assembly> assemblies)
    {
        assemblies.ForEach(x => x.ExportedTypes.Where(x =>
                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>().ToList().ForEach(installer => installer.InstallServices(services, configuration)));
        
    }
}