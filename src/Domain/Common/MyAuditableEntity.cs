using System;

namespace Domain.Common
{
    public class MyAuditableEntity
    {
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
