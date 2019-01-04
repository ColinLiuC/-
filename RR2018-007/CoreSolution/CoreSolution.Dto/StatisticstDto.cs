using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class StatisticstDto<TGroupKey>
    {
        public TGroupKey Key { get; set; }
        public string KeyDescription { get; set; }
        public decimal Sum { get; set; }
        public decimal AVG { get; set; }
        public int Count { get; set; }

        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }
}
