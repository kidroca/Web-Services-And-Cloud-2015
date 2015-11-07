namespace StudentSystem.DataLink
{
    using System.Data.Entity;
    using StudentSystem.DbModels;

    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext()
            : base("StudentSystemDb")
        {
        }

        public virtual IDbSet<Student> Students { get; set; }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Homework> Homeworks { get; set; }
    }
}
