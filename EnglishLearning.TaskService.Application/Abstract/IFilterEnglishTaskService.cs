using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IFilterEnglishTaskService
    {
        Task<IEnumerable<EnglishTaskDto>> FindAllEnglishTaskAsync(string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null);
        Task<EnglishTaskDto> FindRandomEnglishTaskAsync(string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null);
        
        Task<IEnumerable<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null);
        Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null);
    }
}