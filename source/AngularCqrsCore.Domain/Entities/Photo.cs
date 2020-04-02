using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Photo : AuditableEntity
    {
        public Photo()
        {
            DateAdded = DateTime.Now;
        }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public string  PublicId { get; set; }
        public DateTime DateAdded { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}