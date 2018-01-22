using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Contracts;

namespace SF.Remoting
{
    public class ValuesService : IValuesService
    {
        public static IList<ValuesModel> Store = new List<ValuesModel>();

        public Task<IEnumerable<ValuesModel>> GetAllAsync()
        {
            return Task.FromResult(Store.AsEnumerable());
        }

        public Task<ValuesModel> GetAsync(string key)
        {
            return Task.FromResult(Store.Single(x => x.Key == key));
        }

        public Task SetAsync(ValuesModel model)
        {
            Store.Add(model);

            return Task.CompletedTask;
        }
    }
}
