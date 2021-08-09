using System;

namespace Cohesion.Core
{
    public abstract class Auditable
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public Auditable()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }
    }
}
