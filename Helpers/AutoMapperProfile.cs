using Atiendeme.Data.Entities;
using AutoMapper;
using portar_proyectos_api.Data.Entities;
using portar_proyectos_api.Dtos;
using WebApi.Dtos;
namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentDto>();
        }
    }
}