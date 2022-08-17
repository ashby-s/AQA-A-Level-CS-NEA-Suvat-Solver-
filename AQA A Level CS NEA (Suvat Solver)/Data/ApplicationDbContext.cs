namespace AQA_A_Level_CS_NEA__Suvat_Solver_.Data
{
    using Microsoft.EntityFrameworkCore;
    using AQA_A_Level_CS_NEA__Suvat_Solver_.Models;
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsertoCourses>()
            .HasKey(c => new { c.UserId, c.CourseId });
            modelBuilder.Entity<QuestiontoCourses>()
            .HasKey(c => new { c.QuestionId, c.CourseId });
        }


        public DbSet<User> User { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<UsertoCourses> UsertoCourses { get; set; }

        public DbSet<QuestiontoCourses> QuestiontoCourses { get; set; }
    }
}
