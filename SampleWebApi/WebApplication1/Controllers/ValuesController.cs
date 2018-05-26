namespace WebApplication1.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Repository.Entity;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values 
        [HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        public ActionResult<IEnumerable<Values>> Get()
        {
            // return new string[] { "value1", "value2" };

            return new[] { new Values() { Values1 = "value1", Values2 = "value2" } };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Values> Get(int id)
        {
            return new Values() { Values1 = "value" + id, Values2 = "value2" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            // new ValueService().Calculate();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
