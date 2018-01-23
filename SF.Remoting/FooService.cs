using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Contracts;

namespace SF.Remoting
{
    public class FooService : IFooService
    {
        public static IList<FooModel> Store = new List<FooModel>();

        public Task<IEnumerable<FooModel>> GetAllAsync()
        {
            return Task.FromResult(Store.AsEnumerable());
        }

        public Task<FooModel> GetAsync(string key)
        {
            return Task.FromResult(Store.Single(x => x.Key == key));
        }

        public Task SetAsync(FooModel model)
        {
            Store.Add(model);

            return Task.CompletedTask;
        }
    }
}
