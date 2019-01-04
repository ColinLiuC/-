using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
  public  class DataDictionaryDto: EntityBaseFullDto
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public string Description { get; set; }
        public int Sort { get; set; }
        public string Tips { get; set; }
        public int? CustomType { get; set; }
        public string CustomAttributes { get; set; }
        public Guid? ParentId { get; set; }

    }
}
