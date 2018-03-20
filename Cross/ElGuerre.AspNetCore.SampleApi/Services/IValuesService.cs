using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElGuerre.AspNetCore.SampleApi.Services
{
    public interface IValuesService
    {
        Task<IEnumerable<string>> GetAll();

        // GET api/values/5
        Task<string> GetById(int id);

        // POST api/values
        Task Save(string value);

        Task Delete(int id);
    }
}
