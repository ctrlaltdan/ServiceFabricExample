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
    public class ValuesController : Controller
    {
        private readonly IValuesService _service;

        public ValuesController()
        {
            var factory = new ServiceProxyFactory(c => new FabricTransportServiceRemotingClientFactory(
                serializationProvider: new ServiceRemotingJsonSerializationProvider()));

            _service = factory.CreateServiceProxy<IValuesService>(new Uri("fabric:/SF.Example/Remoting"),
                new ServicePartitionKey(),
                TargetReplicaSelector.Default,
                "ValuesListener");
        }
        
        [HttpGet]
        public Task<IEnumerable<ValuesModel>> Get()
        {
            return _service.GetAllAsync();
        }

        [HttpGet("{key}")]
        public Task<ValuesModel> Get(string key)
        {
            return _service.GetAsync(key);
        }
        
        [HttpPut("{key}")]
        public async Task Put(string key, [FromBody]RequestBody body)
        {
            try
            {
                await _service.SetAsync(new ValuesModel
                {
                    Key = key,
                    Value = body.Value
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        
        public class RequestBody
        {
            public string Value { get; set; }
        }
    }
}
