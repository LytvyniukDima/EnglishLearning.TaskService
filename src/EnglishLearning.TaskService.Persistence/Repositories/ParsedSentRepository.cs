using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities.TextAnalyze;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using EnglishLearning.Utilities.Persistence.Mongo.Repositories;

namespace EnglishLearning.TaskService.Persistence.Repositories
{
    internal class ParsedSentRepository : BaseMongoRepository<ParsedSent, string>, IParsedSentRepository
    {
        public ParsedSentRepository(MongoContext mongoContext)
            : base(mongoContext)
        {
        }

        public async Task AddAsync(IReadOnlyCollection<ParsedSent> sents)
        {
            await _collection.InsertManyAsync(sents);
        }
    }
}