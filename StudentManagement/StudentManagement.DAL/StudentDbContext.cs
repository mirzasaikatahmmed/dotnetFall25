using Microsoft.EntityFrameworkCore;

namespace StudentManagement.DAL
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");
                entity.HasKey(e => e.StudentId);
                entity.Property(e => e.StudentId)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.StudentName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.StudentEmail).IsRequired().HasMaxLength(100);
            });
        }
    }
}
