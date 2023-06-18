
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Obs.API;
using Obs.Common.Infrastructure.Installers;
using Obs.Domain.Options;
using Obs.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvcCore();
builder.Services.InstallServicesInAssembly(builder.Configuration,AssemblyMarker.Assemblies);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ConfigureSwagger(app);

//app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await ConfigureDatabaseMigration(app);

app.Run();

void ConfigureSwagger(IApplicationBuilder app)
{
    var swaggerOptions = app.ApplicationServices.GetRequiredService<IOptions<SwaggerOptions>>().Value;
    
    app.UseSwagger(option =>
    {
        option.RouteTemplate = swaggerOptions.JsonRoute;
    });
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
    });
}


async Task ConfigureDatabaseMigration(IApplicationBuilder app)
{
            
    using var serviceScope = app.ApplicationServices.CreateScope();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    await dbContext.Database.MigrateAsync();
    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roleNames = new[] { "Admin", "Poster" };

    for (int i = 0; i < roleNames.Length; i++)
    {
        if (!await roleManager.RoleExistsAsync(roleNames[i]))
        {
            var adminRole = new IdentityRole(roleNames[i]);
            await roleManager.CreateAsync(adminRole);
        }
    }
}