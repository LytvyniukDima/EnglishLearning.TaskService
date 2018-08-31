using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IFilterEnglishTaskService
    {
        Task<IEnumerable<EnglishTaskDto>> FindAllEnglishTaskAsync(Expression<Func<EnglishTaskInfoDto, bool>> filter);
        Task<IEnumerable<EnglishTaskDto>> FindAllEnglishTaskAsync(string[] taskType, string[] grammarPart, string[] englishLevels);
        
        Task<EnglishTaskDto> FindRandomEnglishTaskAsync(Expression<Func<EnglishTaskInfoDto, bool>> filter);
        Task<EnglishTaskDto> FindRandomEnglishTaskAsync(string[] taskType, string[] grammarPart, string[] englishLevels);
        
        Task<IEnumerable<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(Expression<Func<EnglishTaskInfoDto, bool>> filter);
        Task<IEnumerable<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(string[] taskType, string[] grammarPart, string[] englishLevels);
        
        Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(Expression<Func<EnglishTaskInfoDto, bool>> filter);
        Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(string[] taskType, string[] grammarPart, string[] englishLevels);
    }
}