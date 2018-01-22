using System;
using System.Collections.Generic;
using System.Fabric;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using SF.Contracts;

namespace SF.Remoting
{
    internal sealed class Listener : StatelessService
    {
        public Listener(StatelessServiceContext context)
            : base(context)
        {
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[]
            {
                CreateListenerFor(() => new ValuesService(), "ValuesEndpoint", "ValuesListener")
            };
        }

        private static ServiceInstanceListener CreateListenerFor(Func<IService> service, string endpointName, string listenerName)
        {
            var settings = new FabricTransportRemotingListenerSettings { EndpointResourceName = endpointName };
            return new ServiceInstanceListener(context => 
                new FabricTransportServiceRemotingListener(
                    context, 
                    service.Invoke(), 
                    settings,
                    new ServiceRemotingJsonSerializationProvider()),
                listenerName);
        }
    }
}
