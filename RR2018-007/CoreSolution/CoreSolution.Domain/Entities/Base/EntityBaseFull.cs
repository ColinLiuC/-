using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CoreSolution.Domain.Entities.Base
{
    public class EntityBaseFull : Entity<Guid>, IFullEntity
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public override bool IsDeleted { get; set; } = false;
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
