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
                .ForMember(e => e.TaskType, 
                    opt => opt.MapFrom(x => (TaskType) Enum.Parse(typeof(TaskType), x.TaskType)))
                .ForMember(e => e.GrammarPart,
                    opt => opt.MapFrom(x => (GrammarPart) Enum.Parse(typeof(GrammarPart), x.GrammarPart)))
                .ForMember(e => e.EnglishLevel,
                    opt => opt.MapFrom(x => (EnglishLevel) Enum.Parse(typeof(EnglishLevel), x.EnglishLevel)));
            
            CreateMap<EnglishTaskDto, EnglishTask>()
                .ForMember(e => e.TaskType, 
                    opt => opt.MapFrom(x => (TaskType) Enum.Parse(typeof(TaskType), x.TaskType)))
                .ForMember(e => e.GrammarPart,
                    opt => opt.MapFrom(x => (GrammarPart) Enum.Parse(typeof(GrammarPart), x.GrammarPart)))
                .ForMember(e => e.EnglishLevel,
                    opt => opt.MapFrom(x => (EnglishLevel) Enum.Parse(typeof(EnglishLevel), x.EnglishLevel)));
            
            CreateMap<EnglishTask, EnglishTaskDto>()
                .ForMember(e => e.TaskType, opt => opt.MapFrom(x => x.TaskType.ToString()))
                .ForMember(e => e.GrammarPart, opt => opt.MapFrom(x => x.GrammarPart.ToString()))
                .ForMember(e => e.EnglishLevel, opt => opt.MapFrom(x => x.EnglishLevel.ToString()));
            
            CreateMap<EnglishTask, EnglishTaskInfoDto>()
                .ForMember(e => e.TaskType, opt => opt.MapFrom(x => x.TaskType.ToString()))
                .ForMember(e => e.GrammarPart, opt => opt.MapFrom(x => x.GrammarPart.ToString()))
                .ForMember(e => e.EnglishLevel, opt => opt.MapFrom(x => x.EnglishLevel.ToString()));
        }
    }
}