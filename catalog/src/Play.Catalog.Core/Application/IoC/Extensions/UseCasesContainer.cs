namespace Play.Catalog.Core.Application.IoC.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Domain.AggregateModels.ItemModel;
    using Microsoft.Extensions.DependencyInjection;

    internal static class UseCasesContainer
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            var exportedTypes = Assembly.GetExecutingAssembly().ExportedTypes;
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
    }
}