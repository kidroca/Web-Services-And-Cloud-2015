namespace ArtistsServices.WebApi
{
    using System.Data.Entity;
    using DatabaseLink;
    using DatabaseLink.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ArtistsSystemDbContext, Configuration>());

            ArtistsSystemDbContext.Create().Database.Initialize(true);
        }
    }
}