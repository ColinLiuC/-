using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto.Base
{
    public class EntityBaseFullDto : EntityDto<Guid>, IFullEntityDto
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
       
    }
}
