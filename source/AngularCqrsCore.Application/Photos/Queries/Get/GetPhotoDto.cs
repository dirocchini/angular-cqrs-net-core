using System;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Photos.Queries.Get
{
    public class GetPhotoDto : IMapFrom<Photo>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public bool isApproved { get; set; }
        public string PublicId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}