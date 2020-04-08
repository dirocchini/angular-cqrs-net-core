using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Messages.Queries.GetMessage
{
    public class MessageDto : IMapFrom<Message>
    {
        public int Id { get; set; }
    }
}