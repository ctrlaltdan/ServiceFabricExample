using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace SF.Contracts
{
    public interface IBarService : IService
    {
        Task<IEnumerable<BarModel>> GetAllAsync();

        Task<BarModel> GetAsync(Guid key);

        Task SetAsync(BarModel model);
    }
}
