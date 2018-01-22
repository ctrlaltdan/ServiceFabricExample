using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace SF.Contracts
{
    public interface IValuesService : IService
    {
        Task<IEnumerable<ValuesModel>> GetAllAsync();

        Task<ValuesModel> GetAsync(string key);

        Task SetAsync(ValuesModel model);
    }
}
