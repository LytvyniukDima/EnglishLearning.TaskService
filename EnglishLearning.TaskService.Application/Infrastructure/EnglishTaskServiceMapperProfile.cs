using System;
using AutoMapper;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Infrastructure
{
    public class EnglishTaskServiceMapperProfile : Profile
    {
        public EnglishTaskServiceMapperProfile()
        {
            CreateMap<EnglishTaskCreateDto, EnglishTask>()
                .ForMember("TaskType", 
                    opt => opt.MapFrom(x => (TaskType) Enum.Parse(typeof(TaskType), x.TaskType)))
                .ForMember("GrammarPart",
                    opt => opt.MapFrom(x => (GrammarPart) Enum.Parse(typeof(GrammarPart), x.GrammarPart)))
                .ForMember("EnglishLevel",
                    opt => opt.MapFrom(x => (EnglishLevel) Enum.Parse(typeof(EnglishLevel), x.GrammarPart)));
            
            CreateMap<EnglishTaskDto, EnglishTask>()
                .ForMember("TaskType", 
                    opt => opt.MapFrom(x => (TaskType) Enum.Parse(typeof(TaskType), x.TaskType)))
                .ForMember("GrammarPart",
                    opt => opt.MapFrom(x => (GrammarPart) Enum.Parse(typeof(GrammarPart), x.GrammarPart)))
                .ForMember("EnglishLevel",
                    opt => opt.MapFrom(x => (EnglishLevel) Enum.Parse(typeof(EnglishLevel), x.GrammarPart)));

            CreateMap<EnglishTask, EnglishTaskDto>()
                .ForMember("TaskType", opt => opt.MapFrom(x => x.TaskType.ToString()))
                .ForMember("GrammarPart", opt => opt.MapFrom(x => x.GrammarPart.ToString()))
                .ForMember("EnglishLevel", opt => opt.MapFrom(x => x.EnglishLevel.ToString()));
            
            CreateMap<EnglishTask, EnglishTaskInfoDto>()
                .ForMember("TaskType", opt => opt.MapFrom(x => x.TaskType.ToString()))
                .ForMember("GrammarPart", opt => opt.MapFrom(x => x.GrammarPart.ToString()))
                .ForMember("EnglishLevel", opt => opt.MapFrom(x => x.EnglishLevel.ToString())); 
        }
    }
}