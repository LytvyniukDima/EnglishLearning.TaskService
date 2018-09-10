﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("api/tasks")]
    public class EnglishTaskController : Controller
    {
        private readonly IEnglishTaskService _englishTaskService;
        private readonly IMapper _mapper;

        public EnglishTaskController(IEnglishTaskService englishTaskService, EnglishTaskWebMapper englishTaskWebMapper)
        {
            _englishTaskService = englishTaskService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<EnglishTaskDto> englishTaskDtos = await _englishTaskService.GetAllEnglishTaskAsync();
            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskDto>, IEnumerable<EnglishTaskModel>>(englishTaskDtos);

            return Ok(englishTaskModels);
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            EnglishTaskDto englishTask = await _englishTaskService.GetByIdEnglishTaskAsync(id);
            if (englishTask == null)
                return NotFound();

            var englishTaskModel = _mapper.Map<EnglishTaskDto, EnglishTaskModel>(englishTask);
            
            return Ok(englishTaskModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EnglishTaskCreateModel englishTaskCreateModel)
        {
            var englishTaskCreateDto = _mapper.Map<EnglishTaskCreateModel, EnglishTaskCreateDto>(englishTaskCreateModel);
            
            await _englishTaskService.CreateEnglishTaskAsync(englishTaskCreateDto);

            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EnglishTaskCreateModel englishTaskCreateModel)
        {
            var englishTaskCreateDto = _mapper.Map<EnglishTaskCreateModel, EnglishTaskCreateDto>(englishTaskCreateModel);

            bool result = await _englishTaskService.UpdateEnglishTaskAsync(id, englishTaskCreateDto);

            if (result == false)
                return BadRequest();
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool result = await _englishTaskService.DeleteByIdEnglishTaskAsync(id);

            if (result == false)
                return BadRequest();
            
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            bool result = await _englishTaskService.DeleteAllEnglishTaskAsync();

            if (result == false)
                return BadRequest();
            
            return Ok();
        }
        
        [AllowAnonymous]
        [HttpGet("/info")]
        public async Task<IActionResult> GetAllInfo()
        {
            IEnumerable<EnglishTaskInfoDto> englishTaskDtos = await _englishTaskService.GetAllEnglishTaskInfoAsync();
            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskInfoDto>, IEnumerable<EnglishTaskInfoModel>>(englishTaskDtos);

            return Ok(englishTaskModels);
        }
        
        [AllowAnonymous]
        [HttpGet("/info/{id}")]
        public async Task<IActionResult> GetInfoById(string id)
        {
            EnglishTaskDto englishTask = await _englishTaskService.GetByIdEnglishTaskAsync(id);
            if (englishTask == null)
                return NotFound();

            var englishTaskModel = _mapper.Map<EnglishTaskDto, EnglishTaskModel>(englishTask);
            
            return Ok(englishTaskModel);
        }
        
        [AllowAnonymous]
        [HttpGet("/filter")]
        public async Task<ActionResult> GetAllByFilter(
            [FromQuery] string[] tasktype, 
            [FromQuery] string[] grammarPart, 
            [FromQuery] string[] englishLevel)
        {
            IEnumerable<EnglishTaskDto> englishTakDtos = await _englishTaskService.FindAllEnglishTaskAsync(tasktype, grammarPart, englishLevel);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
        
        [AllowAnonymous]
        [HttpGet("/info/filter")]
        public async Task<ActionResult> GetAllInfoByFilter(
            [FromQuery] string[] tasktype, 
            [FromQuery] string[] grammarPart, 
            [FromQuery] string[] englishLevel)
        {
            IEnumerable<EnglishTaskInfoDto> englishTakDtos = await _englishTaskService.FindAllInfoEnglishTaskAsync(tasktype, grammarPart, englishLevel);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
    }
}