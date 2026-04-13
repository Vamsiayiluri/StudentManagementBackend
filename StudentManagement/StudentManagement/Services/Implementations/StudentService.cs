namespace StudentManagement.Services.Implementations
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using StudentManagement.DTOs;
    using StudentManagement.Models;
    using StudentManagement.Repositories.Interfaces;
    using StudentManagement.Services.Interfaces;

    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;

        public StudentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<StudentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<StudentDto>> GetAllAsync(StudentQueryParams query)
        {
            _logger.LogInformation("Fetching students with pagination");

            var queryable = _unitOfWork.Students.Query(s =>
                string.IsNullOrEmpty(query.Search) ||
                s.Name.Contains(query.Search));

            var students = await queryable
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching student with Id: {Id}", id);
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null)
            {
                _logger.LogWarning("Student with Id {Id} not found", id);
                throw new KeyNotFoundException($"Student with Id {id} not found");
            }
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> CreateAsync(StudentDto dto)
        {
            _logger.LogInformation("Creating new student");
            var student = _mapper.Map<Student>(dto);

            await _unitOfWork.Students.AddAsync(student);
            _logger.LogInformation("Student created successfully");


            return _mapper.Map<StudentDto>(student);
        }

        public async Task<bool> UpdateAsync(int id, StudentDto dto)
        {
            _logger.LogInformation("Updating student with Id: {Id}", id);

            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
            {
                _logger.LogWarning("Update failed. Student with Id {Id} not found", id);
                return false;
            }

            _mapper.Map(dto, student);

            await _unitOfWork.Students.UpdateAsync(student);

            _logger.LogInformation("Student updated successfully");

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting student with Id: {Id}", id);

            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
            {
                _logger.LogWarning("Delete failed. Student with Id {Id} not found", id);
                return false;
            }

            await _unitOfWork.Students.DeleteAsync(student);

            _logger.LogInformation("Student deleted successfully");

            return true;
        }
    }
}