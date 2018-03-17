using ElGuerre.AspNetCore.SampleApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IElGuerre.AspNetCore.SampleApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IValuesServices _valuesServices;

        public ValuesController(IValuesServices valuesServices) => _valuesServices = valuesServices;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _valuesServices.GetAll();
            // return Ok(new ApiResult<IEnumerable<string>>(result) { IsValid = true });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _valuesServices.GetById(id);
            // return Ok(new ApiResult<string>(result) { IsValid = true });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]string value)
        {
            await _valuesServices.Save(value);
            // return Ok(new ApiResult<IEnumerable<string>>() { IsValid = true });
            return Ok(true);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _valuesServices.Delete(id);
            // return Ok(new ApiResult<IEnumerable<string>>() { IsValid = true });
            return Ok(true);
        }
    }
}
