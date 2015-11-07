namespace ArtistsServices.DatabaseLink
{
    using System.Data.Entity;
    using DatabaseModels;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ArtistsSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        public ArtistsSystemDbContext()
            : base("ArtistsSystem")
        {
        }

        public virtual IDbSet<Artist> Artists { get; set; }

        public virtual IDbSet<Album> Albums { get; set; }

        public virtual IDbSet<Song> Songs { get; set; }

        public static ArtistsSystemDbContext Create()
        {
            return new ArtistsSystemDbContext();
        }
    }
}
