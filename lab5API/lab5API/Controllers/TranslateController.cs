using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab5API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TranslateController : ControllerBase
    {
        // GET: api/Translate
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Translate/5
        [HttpGet("{id:int}", Name = "getInt")]
        [ActionName("GetInt")]
        public string Get(int id)
        {
            int calc = id + 100;
            return "Your number plus 100 is " + calc + " where " + id + " is the number you calculated.";
        }

        // GET: api/Translate/String
        [HttpGet("{input}", Name = "getString")]
        [ActionName("GetString")]
        public string GetString(string input)
        {
            return "You sent the string " + input;
        }

        [HttpGet]
        [ActionName("GetName")]
        public string GetName([FromQuery]string name) 
        { 
            return "Your Name is " + name; 
        }

        // POST: api/Translate
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Translate/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
