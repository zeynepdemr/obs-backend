using System.Reflection;
using Obs.Application;
using Obs.Common;
using Obs.Domain;
using Obs.Persistence;

namespace Obs.API;

public class AssemblyMarker : IAssemblyMarker
{
    public static List<Assembly> Assemblies = new List<Assembly>
    {
        typeof(CommonAssemblyMarker).Assembly,
        typeof(DomainAssemblyMarker).Assembly,
        typeof(ApplicationAssemblyMarker).Assembly,
        typeof(PersistenceAssemblyMarker).Assembly,
        typeof(AssemblyMarker).Assembly
    };
}