using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto.MyModel
{
    public class TreeModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public Guid? pId { get; set; }
        public string type { get; set; }
    }
}
