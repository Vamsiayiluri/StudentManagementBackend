using StudentManagement.Data;
using StudentManagement.Models;
using StudentManagement.Repositories.Interfaces;

namespace StudentManagement.Repositories.Implementations
{
    using Microsoft.EntityFrameworkCore.Storage;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Student> Students { get; }
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<RefreshToken> RefreshTokens { get; }
        public IGenericRepository<Course> Courses { get; }
        public IGenericRepository<Enrollment> Enrollments { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Students = new GenericRepository<Student>(_context);
            Users = new GenericRepository<User>(_context);
            RefreshTokens = new GenericRepository<RefreshToken>(_context);
            Courses = new GenericRepository<Course>(_context);
            Enrollments = new GenericRepository<Enrollment>(_context);
        }

       
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}