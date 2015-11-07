namespace ArtistsServices.DatabaseModels
{
    using System.ComponentModel.DataAnnotations;

    public class Song
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public int Year { get; set; }

        public Genre? Genre { get; set; }

        public int AlubumId { get; set; }

        public virtual Album Album { get; set; }
    }
}
