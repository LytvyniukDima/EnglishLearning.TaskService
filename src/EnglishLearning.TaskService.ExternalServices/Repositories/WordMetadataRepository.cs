using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.Dictionary.Client;
using EnglishLearning.Dictionary.Web.Contracts.Metadata;
using EnglishLearning.TaskService.ExternalServices.Abstract;
using EnglishLearning.TaskService.ExternalServices.Contracts;
using EnglishLearning.TaskService.ExternalServices.Infrastructure;

namespace EnglishLearning.TaskService.ExternalServices.Repositories
{
    internal class WordMetadataRepository : IWordMetadataRepository
    {
        private readonly WordMetadataClient _client;

        private readonly IMapper _mapper;
        
        public WordMetadataRepository(
            WordMetadataClient client,
            ExternalServicesMapper mapper)
        {
            _client = client;
            _mapper = mapper.Mapper;
        }
        
        public async Task<IReadOnlyList<WordMetadataContract>> GetAsync(IReadOnlyList<string> words)
        {
            var query = new WordMetadataQuery
            {
                Words = words,
            };
            
            var metadata = await _client.GetWordMetadata(query);

            return _mapper.Map<IReadOnlyList<WordMetadataContract>>(metadata);
        }
    }
}