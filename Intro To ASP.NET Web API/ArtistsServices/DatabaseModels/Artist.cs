namespace ArtistsServices.DatabaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Artist
    {
        private ICollection<Album> albums;

        private ICollection<Song> singles;

        public Artist()
        {
            this.albums = new HashSet<Album>();

            this.singles = new HashSet<Song>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public DateTime BornOn { get; set; }

        public virtual ICollection<Album> Albums
        {
            get { return this.albums; }

            set { this.albums = value; }
        }

        public virtual ICollection<Song> Singles
        {
            get { return this.singles; }

            set { this.singles = value; }
        }
    }
}
