﻿using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Message : AuditableEntity
    {
        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int RecipientId { get; set; }
        public User Recipient { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }

        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}