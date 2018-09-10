using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("api/englishtasks/filter")]
    public class FilterEnglishTaskController : Controller
    {
        private readonly IFilterEnglishTaskService _filterService;
        private readonly IMapper _mapper;
        
        public FilterEnglishTaskController(
            IFilterEnglishTaskService filterService,
            EnglishTaskWebMapper englishTaskWebMapper)
        {
            _filterService = filterService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        // GET api/values
        [HttpGet]
        public async Task<ActionResult> GetAllByFilter(
            [FromQuery] string[] tasktype, 
            [FromQuery] string[] grammarPart, 
            [FromQuery] string[] englishLevel)
        {
            IEnumerable<EnglishTaskDto> englishTakDtos = await _filterService.FindAllEnglishTaskAsync(tasktype, grammarPart, englishLevel);
            if (englishTakDtos.Any())
                return NotFound();

            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
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