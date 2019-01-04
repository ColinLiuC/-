using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 数据字典
    /// </summary>
  public  class DataDictionary:EntityBaseFull
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
