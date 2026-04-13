using AutoMapper;
using StudentManagement.DTOs;
using StudentManagement.Models;
namespace StudentManagement.Mapping
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
        }
    }
}