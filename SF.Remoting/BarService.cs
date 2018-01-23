using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Contracts;

namespace SF.Remoting
{
    public class BarService : IBarService
    {
        public static IList<BarModel> Store = new List<BarModel>();

        public Task<IEnumerable<BarModel>> GetAllAsync()
        {
            return Task.FromResult(Store.AsEnumerable());
        }

        public Task<BarModel> GetAsync(Guid key)
        {
            return Task.FromResult(Store.Single(x => x.Key == key));
        }

        public Task SetAsync(BarModel model)
        {
            Store.Add(model);

            return Task.CompletedTask;
        }
    }
}
