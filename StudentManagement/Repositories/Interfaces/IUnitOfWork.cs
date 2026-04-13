using System.Threading.Tasks;

namespace StudentManagement.Repositories.Interfaces
{
    using StudentManagement.Models;

    using Microsoft.EntityFrameworkCore.Storage;

    public interface IUnitOfWork
    {
        IGenericRepository<Student> Students { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<RefreshToken> RefreshTokens { get; }
        IGenericRepository<Course> Courses { get; }
        IGenericRepository<Enrollment> Enrollments { get; }

        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}