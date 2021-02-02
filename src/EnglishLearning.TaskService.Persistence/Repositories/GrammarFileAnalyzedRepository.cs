using System;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities.TextAnalyze;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using EnglishLearning.Utilities.Persistence.Mongo.Repositories;

namespace EnglishLearning.TaskService.Persistence.Repositories
{
    public class GrammarFileAnalyzedRepository : BaseMongoRepository<GrammarFileAnalyzed, Guid>, IGrammarFileAnalyzedRepository
    {
        public GrammarFileAnalyzedRepository(MongoContext mongoContext)
            : base(mongoContext)
        {
        }
    }
}