using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.ExternalServices.Contracts;

namespace EnglishLearning.TaskService.ExternalServices.Abstract
{
    public interface IWordMetadataRepository
    {
        Task<IReadOnlyList<WordMetadataContract>> GetAsync(IReadOnlyList<string> words);
    }
}