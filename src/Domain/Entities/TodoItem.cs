﻿using Cook_Log.Domain.Common;
using Cook_Log.Domain.Enums;
using System;

namespace Cook_Log.Domain.Entities
{
    public class TodoItem : AuditableEntity
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public bool Done { get; set; }
        public DateTime? Reminder { get; set; }
        public PriorityLevel Priority { get; set; }
        public TodoList List { get; set; }
    }
}
