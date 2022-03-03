namespace Play.Common.UseCases.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            var exportedTypes = TryGetExportedTypes();
            foreach (var exported in exportedTypes)
            {
                var interfaceType = GetInterfaceType(exported);
                if (interfaceType == null) continue;

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

        private static IEnumerable<Type> TryGetExportedTypes()
        {
            var entryAssemblyExportedTypes = Assembly.GetEntryAssembly()?.ExportedTypes.ToList();
            var executingAssemblyExportedTypes = Assembly.GetExecutingAssembly().ExportedTypes.ToList();

            var exportedTypes = new List<Type>();
            if (entryAssemblyExportedTypes is { Count: > 0 })
                exportedTypes.AddRange(entryAssemblyExportedTypes);

            if (executingAssemblyExportedTypes is { Count: > 0 })
                exportedTypes.AddRange(executingAssemblyExportedTypes);
            
            return exportedTypes;
        }
    }
}