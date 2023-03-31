using Microsoft.EntityFrameworkCore;
using WestcoastAPI.Models;

namespace WestcoastAPI.Data
{
    public class WestcoastContext : DbContext
    {
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<StudentCourse> StudentCourse => Set<StudentCourse>();

        public WestcoastContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
            .HasKey(sc => new{ sc.CourseId, sc.StudentId });

            modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Student)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.CourseId);
        }
    }
}