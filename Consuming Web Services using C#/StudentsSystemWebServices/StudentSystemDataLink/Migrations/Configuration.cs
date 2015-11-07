namespace StudentSystem.DataLink.Migrations
{
    using System.Data.Entity.Migrations;
    using StudentSystem.DbModels;

    public sealed class Configuration : DbMigrationsConfiguration<StudentsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = "StudentSystem.DataLink.StudentsDbContext";
        }

        protected override void Seed(StudentsDbContext context)
        {
            var seedDataGen = new SeedingValues();

            Student[] students = seedDataGen.GetStudents().Result;

            Course[] courses = seedDataGen.GetCourses().Result;

            Homework[] homeworks = seedDataGen.GetHomeworks().Result;

            seedDataGen.MakeDataAssociations(students, courses, homeworks);

            context.Students.AddOrUpdate(s => new { s.FirstName, s.LastName, s.Email, s.PhoneNumber }, students);
        }
    }
}
