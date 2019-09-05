using System;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using EnglishLearning.Utilities.Persistence.Mongo.Repositories;

namespace EnglishLearning.TaskService.Persistence.Repositories
{
    public class UserInformationMongoRepository : BaseMongoRepository<UserInformation, Guid>, IUserInformationRepository
    {
        public UserInformationMongoRepository(MongoContext mongoContext) 
            : base(mongoContext)
        {
        }
    }
}
