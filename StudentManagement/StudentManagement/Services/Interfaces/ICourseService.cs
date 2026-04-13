using StudentManagement.DTOs;

namespace StudentManagement.Services.Interfaces
{
    public interface ICourseService
    {
        Task CreateCourseAsync(CreateCourseDto dto);
        Task EnrollStudentAsync(EnrollStudentDto dto);
        Task<List<StudentWithCoursesDto>> GetStudentsWithCourses();
        Task<object> GetCourseStats();
        Task<object> GetStudentsByCourse(string courseName);
    }
}
