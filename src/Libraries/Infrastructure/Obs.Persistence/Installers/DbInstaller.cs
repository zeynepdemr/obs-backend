using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Obs.Common.Infrastructure.Installers;
using Obs.Persistence.Data;

namespace Obs.Persistence.Installers;

public class DbInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),b => b.MigrationsAssembly("Obs.API")));

        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DataContext>();
    }
}