using System.Threading;
using Microsoft.ServiceFabric.Services.Runtime;

namespace SF.WebApi
{
    internal static class Program
    {
        private static void Main()
        {
            ServiceRuntime
                .RegisterServiceAsync("SF.WebApi.Type", context => new Listener(context))
                .GetAwaiter()
                .GetResult();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
