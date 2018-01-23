using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace SF.Contracts
{
    public interface IFooService : IService
    {
        Task<IEnumerable<FooModel>> GetAllAsync();

        Task<FooModel> GetAsync(string key);

        Task SetAsync(FooModel model);
    }
}
