using Microsoft.EntityFrameworkCore;
using WebPractice.Models;
namespace WebPractice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // 여기에 DB 테이블들 추가
        public DbSet<User> Users { get; set; }
    }
}
