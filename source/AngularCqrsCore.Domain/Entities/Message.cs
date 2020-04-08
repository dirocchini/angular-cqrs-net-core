using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Message : AuditableEntity
    {
        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int RecepientId { get; set; }
        public User Recepient { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }

        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}