namespace Play.Common.UseCases
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public abstract class UseCaseDefinition<TRequest> : IUseCaseDefinition<TRequest>
        where TRequest : IUseCaseRequest
    {
        protected UseCaseDefinition(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public abstract Task<Response> Execute(TRequest request);
        
        protected ILogger Logger { get; }
    }
}