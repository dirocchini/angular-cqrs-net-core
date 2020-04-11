using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Application.Messages.Queries.GetMessage
{
    public class MessageDto : IMapFrom<Message>
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public string SenderKnownAs { get; set; }
        public string SenderPhotoUrl { get; set; }
        [JsonIgnore]
        public User Sender { get; set; }

        public int RecipientId { get; set; }
        public string RecipientKnownAs { get; set; }
        public string RecipientPhotoUrl { get; set; }
        [JsonIgnore]
        public User Recipient { get; set; }


        public string Content { get; set; }

        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }

        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageDto>()
                .ForMember(d => d.SenderKnownAs, opt => opt.MapFrom(s => (s.Sender == null ? "not found" : s.Sender.KnownAs)))
                .ForMember(d => d.SenderPhotoUrl, opt => opt.MapFrom(s => (s.Sender == null ? "" : GetMainPhoto(s.Sender))))
                .ForMember(d => d.RecipientKnownAs, opt => opt.MapFrom(s => (s.Recipient == null ? "not found" : s.Recipient.KnownAs)))
                .ForMember(d => d.RecipientPhotoUrl, opt => opt.MapFrom(s => (s.Recipient == null ? "" : GetMainPhoto(s.Recipient))));
        }

        private string GetMainPhoto(User sender)
        {
            var photo = sender.Photos.FirstOrDefault(p => p.IsMain);
            return photo?.Url ?? "";
        }
    }
}