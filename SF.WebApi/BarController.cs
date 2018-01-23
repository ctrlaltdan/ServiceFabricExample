using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using SF.Contracts;

namespace SF.WebApi
{
    [Route("api/[controller]")]
    public class BarController : Controller
    {
        private readonly IBarService _service;

        public BarController()
        {
            var factory = new ServiceProxyFactory(c => new FabricTransportServiceRemotingClientFactory(
                serializationProvider: new ServiceRemotingJsonSerializationProvider()));

            _service = factory.CreateServiceProxy<IBarService>(new Uri("fabric:/SF.Example/Remoting"),
                new ServicePartitionKey(),
                TargetReplicaSelector.Default,
                "BarListener");
        }
        
        [HttpGet]
        public Task<IEnumerable<BarModel>> Get()
        {
            return _service.GetAllAsync();
        }

        [HttpGet("{key:guid}")]
        public Task<BarModel> Get(Guid key)
        {
            return _service.GetAsync(key);
        }
        
        [HttpPost]
        public async Task<BarModel> Post()
        {
            var key = Guid.NewGuid();
            var value = Guid.NewGuid().ToString("N");

            var model = new BarModel
            {
                Key = key,
                Value = value
            };

            await _service.SetAsync(model);

            return model;
        }
    }
}
