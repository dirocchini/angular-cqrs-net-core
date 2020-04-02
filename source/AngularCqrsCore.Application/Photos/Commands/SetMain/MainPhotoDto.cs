using System;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Photos.Commands.SetMain
{
    public class MainPhotoDto : IMapFrom<Photo>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public DateTime DateAdded { get; set; }

    }
}