using Microsoft.EntityFrameworkCore;

namespace Task1.Models
{
    public class TaskContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        public TaskContext()
        {
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-RRC41OD\\SQLEXPRESS;Database=Task;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasMany(m => m.Users).WithOne(l => l.Company).HasForeignKey(m => m.CompanyId);
        }
    }
}
