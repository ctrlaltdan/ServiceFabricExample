using System.Threading;
using Microsoft.ServiceFabric.Services.Runtime;

namespace SF.Remoting
{
    internal static class Program
    {
        private static void Main()
        {
            ServiceRuntime
                .RegisterServiceAsync("SF.Remoting.Type", context => new Listener(context))
                .GetAwaiter()
                .GetResult();

            Thread.Sleep(Timeout.Infinite);   
        }
    }
}
