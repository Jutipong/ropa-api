using AutoMapper;
using WebApi.Entities;
using WebApi.Entities.Models;
using WebApi.Models;

namespace WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ConfigGroupQuestionModel, ConfigGroupQuestion>();
        }
    }
}
