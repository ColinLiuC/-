using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class ServiceApplicationAndEvaluationDto
    {
        //服务申请
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// 服务Id
        /// </summary>
        public Guid ServiceId { get; set; }
        /// <summary>
        /// 当前状态 1-未接收 2-接收未评价 3-未通过 4-已评价
        /// </summary>
        public int? CurrentState { get; set; }

        //服务信息表
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 服务描述
        /// </summary>
        public string ServiceDescription { get; set; }
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceAddress { get; set; }
        /// <summary>
        /// 服务时间描述
        /// </summary>
        public string TimeDescription { get; set; }
        
        

        //服务评价
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
