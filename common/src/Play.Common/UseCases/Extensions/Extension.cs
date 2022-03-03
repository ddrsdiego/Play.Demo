namespace Play.Common.UseCases.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (!assemblies.Any())
                throw new ArgumentException(
                    "No assemblies found to scan. Supply at least one assembly to scan for handlers.");

            assemblies = (assemblies as Assembly[] ?? assemblies).Distinct().ToArray();

            var exportedTypes = TryGetExportedTypes(assemblies);
            foreach (var exported in exportedTypes)
            {
                var interfaceType = GetInterfaceType(exported);
                if (interfaceType == null || exported.IsAbstract) continue;

                services.AddScoped(interfaceType, exported);
            }

            return services;

            static Type? GetInterfaceType(Type type) => Array.Find(type.GetInterfaces(), IsInvalid);

            static bool IsInvalid(Type type)
            {
                var isUseCaseReq = false;
                foreach (var typeArgument in type.GenericTypeArguments)
                {
                    isUseCaseReq = typeArgument
                        .GetInterfaces()
                        .Any(x => x.Name.Equals(nameof(IUseCaseRequest), StringComparison.InvariantCultureIgnoreCase));
                }

                if (!isUseCaseReq)
                    return false;

                return type.IsInterface && type.IsGenericType && isUseCaseReq;
            }
        }

        private static IEnumerable<Type> TryGetExportedTypes(IEnumerable<Assembly> assemblies)
        {
            var exportedTypes = new List<Type>();

            foreach (var assembly in assemblies)
            {
                exportedTypes.AddRange(assembly.ExportedTypes);
            }

            var entryAssemblyExportedTypes = Assembly.GetEntryAssembly()?.ExportedTypes.ToList();
            var executingAssemblyExportedTypes = Assembly.GetExecutingAssembly().ExportedTypes.ToList();

            if (entryAssemblyExportedTypes is { Count: > 0 })
                exportedTypes.AddRange(entryAssemblyExportedTypes);

            if (executingAssemblyExportedTypes is { Count: > 0 })
                exportedTypes.AddRange(executingAssemblyExportedTypes);

            return exportedTypes;
        }
    }
}