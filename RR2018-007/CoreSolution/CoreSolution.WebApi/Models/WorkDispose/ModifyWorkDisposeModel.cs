using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.WorkDispose
{
    /// <summary>
    /// 处理登记参数model
    /// </summary>
    public class ModifyWorkDisposeModel
    {
        public Guid Id;
        public string DisposeUser { get; set; }
        public string DisposeResult { get; set; }
        public DateTime? DisposeTime { get; set; }
    }
}
