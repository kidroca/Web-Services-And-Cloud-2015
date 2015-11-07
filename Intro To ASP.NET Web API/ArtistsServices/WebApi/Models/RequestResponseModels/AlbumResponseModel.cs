namespace ArtistsServices.WebApi.Models.RequestResponseModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using DatabaseModels;

    public class AlbumResponseModel : IMapFrom<Album>, ICustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Producer { get; set; }

        public int Year { get; set; }

        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }

        public string[] Songs { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Album, AlbumResponseModel>()
                .ForMember(a => a.Songs, opts => opts
                    .MapFrom(a => a.Songs.Select(
                        s => s.Title)));
        }
    }
}