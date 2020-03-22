using Domain.Common;

namespace Domain.Entities
{
    public class Photo : AuditableEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        


        public int UserId { get; set; }
        public User User { get; set; }
    }
}