using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("api/filter")]
    public class FilterEnglishTaskController : Controller
    {
        private readonly IFilterEnglishTaskService _filterService;
        private readonly IEnglishTaskService _englishTaskService;
        
        public FilterEnglishTaskController(
            IEnglishTaskService englishTaskService,
            IFilterEnglishTaskService filterService)
        {
            _englishTaskService = englishTaskService;
            _filterService = filterService;
        }
        
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnglishTaskDto>>> Get()
        {
            var model = new EnglishTaskCreateDto
            {
                TaskType = "SimpleBrackets",
                GrammarPart = "PRSimple",
                EnglishLevel = "PreIntermediate",
                Count = 10,
                Example = "example",
                Text = "text",
                Answer = "answer"
            };

            await _englishTaskService.CreateEnglishTaskAsync(model);

            var taskTypes = new string[] {"SimpleBrackets", "CorrectAlternative"};
            var grammarParts = new string[] {"PRSimple", "PRContinuous"};
            var englishLevels = new string[] {"PreIntermediate"};
            
            var englishTasks = await _filterService.FindAllInfoEnglishTaskAsync(taskTypes: taskTypes, grammarParts: grammarParts, englishLevels: englishLevels);
            
            return Ok(englishTasks);
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