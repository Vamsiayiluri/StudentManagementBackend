using Microsoft.EntityFrameworkCore;
using StudentManagement.DTOs;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Services.Implementations
{

    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateCourseAsync(CreateCourseDto dto)
        {
            var course = new Course
            {
                Name = dto.Name
            };

            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EnrollStudentAsync(EnrollStudentDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var student = await _unitOfWork.Students.GetByIdAsync(dto.StudentId);
                if (student == null)
                    throw new Exception("Student not found");

                var course = await _unitOfWork.Courses.GetByIdAsync(dto.CourseId);
                if (course == null)
                    throw new Exception("Course not found");

                var enrollment = new Enrollment
                {
                    StudentId = dto.StudentId,
                    CourseId = dto.CourseId,
                    EnrolledAt = DateTime.UtcNow
                };

                await _unitOfWork.Enrollments.AddAsync(enrollment);
                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<StudentWithCoursesDto>> GetStudentsWithCourses()
        {
            return await _unitOfWork.Students.SelectAsync(
                null,
                s => new StudentWithCoursesDto
                {
                    Name = s.Name,
                    Courses = s.Enrollments
                        .Select(e => e.Course.Name)
                        .ToList()
                }
            );
        }

        public async Task<object> GetCourseStats()
        {
            return await _unitOfWork.Enrollments.Query(e => true)
                .GroupBy(e => e.Course.Name)
                .Select(g => new
                {
                    Course = g.Key,
                    StudentCount = g.Count()
                })
                .ToListAsync();
        }

        public async Task<object> GetStudentsByCourse(string courseName)
        {
            return await _unitOfWork.Enrollments.Query(e => e.Course.Name == courseName)
    .Select(e => new
    {
        Student = e.Student.Name,
        e.EnrolledAt
    })
    .AsNoTracking()
    .ToListAsync();
        }
    }
}
