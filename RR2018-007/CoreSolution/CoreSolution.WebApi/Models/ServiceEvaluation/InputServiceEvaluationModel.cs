using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ServiceEvaluation
{
    public class InputServiceEvaluationModel
    {
        /// <summary>
        /// 对应的服务Id
        /// </summary>
        public Guid ServiceGuid { get; set; }
        /// <summary>
        /// 评价人Guid
        /// </summary>
        public string UserGuid { get; set; }
        /// <summary>
        /// 评价人
        /// </summary>
        public string Evaluator { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string EvaluationContent { get; set; }
        /// <summary>
        /// 评价时间
        /// </summary>
        public DateTime EvaluationDate { get; set; }
        /// <summary>
        /// 评价人头像路径
        /// </summary>
        public string EvaluatorImgPath { get; set; }
        /// <summary>
        /// 满意度
        /// </summary>
        public int Satisfaction { get; set; }
    }
}
