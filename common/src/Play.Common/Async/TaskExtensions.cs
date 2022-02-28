namespace Play.Common.Async
{
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static async ValueTask<T> FastResult<T>(this Task<T> slowTask)
        {
            if (slowTask.IsCompletedSuccessfully)
                return slowTask.Result;

            return await slowTask;
        }
        
        public static async ValueTask<T> FastResult<T>(this ValueTask<T> slowTask)
        {
            if (slowTask.IsCompletedSuccessfully)
                return slowTask.Result;

            return await slowTask;
        }
    }
}