using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public class StudentProfiles : Profile
    {
        public StudentProfiles()
        {
            CreateMap<Student, StudentReadDto>();
            CreateMap<StudentReadDto, Student>();

            CreateMap<Student, StudentCreateDto>();
            CreateMap<StudentCreateDto, Student>();
        }
    }
}