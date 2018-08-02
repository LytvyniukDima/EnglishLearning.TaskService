using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Persistence.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("EnglishLearning");
            var collection = database.GetCollection <EnglishTask>("englishTasks");
            await collection.InsertOneAsync(new EnglishTask
            {
                Count = 10,
                Answer = "answer",
                Example = "example",
                Text = "text",
                EnglishLevel = EnglishLevel.Advanced
            });
            var values = collection
                .Find(new BsonDocument())
                .ToList()
                .Select(x => x.Id.ToString());

            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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