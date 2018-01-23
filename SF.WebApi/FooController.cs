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
    public class FooController : Controller
    {
        private readonly IFooService _service;

        public FooController()
        {
            var factory = new ServiceProxyFactory(c => new FabricTransportServiceRemotingClientFactory(
                serializationProvider: new ServiceRemotingJsonSerializationProvider()));

            _service = factory.CreateServiceProxy<IFooService>(new Uri("fabric:/SF.Example/Remoting"),
                new ServicePartitionKey(),
                TargetReplicaSelector.Default,
                "FooListener");
        }
        
        [HttpGet]
        public Task<IEnumerable<FooModel>> Get()
        {
            return _service.GetAllAsync();
        }

        [HttpGet("{key}")]
        public Task<FooModel> Get(string key)
        {
            return _service.GetAsync(key);
        }
        
        [HttpPost]
        public async Task<FooModel> Post()
        {
            var key = Guid.NewGuid().ToString("N").Substring(0,4);
            var value = Guid.NewGuid().ToString("N");

            var model = new FooModel
            {
                Key = key,
                Value = value
            };

            await _service.SetAsync(model);

            return model;
        }
    }
}
