namespace ArtistsServices.WebApi.Models.RequestResponseModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using DatabaseModels;

    public class ArtistResponseModel : IMapFrom<Artist>, ICustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public DateTime BornOn { get; set; }

        public string[] Albums { get; set; }

        public string[] Singles { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Artist, ArtistResponseModel>()
                .ForMember(s => s.Albums, opts => opts
                    .MapFrom(s => s.Albums.Select(
                        a => a.Title)))
                .ForMember(s => s.Singles, opts => opts
                    .MapFrom(s => s.Singles.Select(
                        single => single.Title)));
        }
    }
}