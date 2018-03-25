using ElGuerre.AspNetCore.Cross.Exception.Exception;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElGuerre.AspNetCore.SampleApi.Services
{
    public class ValuesServices : IValuesServices
    {
        public ValuesServices() { }

        public async Task<IEnumerable<string>> GetAll()
        {
            var result = new string[] { "value1", "value2" };
            try
            {

            }
            catch (Exception e)
            {
                throw new BusinessException("Texto de error a mostrar", e);
            }

            await Task.CompletedTask;
            return result;
        }

        // GET api/values/5
        public async Task<string> GetById(int id)
        {
            var result = string.Format("Value {0}", id);

            if (id < 0)
            {
                throw new BusinessException("Id must be greater than 0");
            }

            await Task.CompletedTask;
            return result;
        }

        // POST api/values
        public async Task Save(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BusinessException("Value cannot be null or empty");
            }

            await Task.CompletedTask;
        }

        public async Task Delete(int id)
        {
            if (id < 0)
            {
                throw new BusinessException("Id must be greater than 0");
            }

            await Task.CompletedTask;
        }
    }
}
