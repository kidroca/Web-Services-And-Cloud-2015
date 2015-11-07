namespace StudentSystem.RestfulApi.App_Start
{
    using System.Data.Entity;
    using System.Linq;
    using DataLink;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<StudentsDbContext, DataLink.Migrations.Configuration>());

            using (var context = new StudentsDbContext())
            {
                context.Students.FirstOrDefault();
            }
        }
    }
}