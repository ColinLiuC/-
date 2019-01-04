using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.QuestionnaireManage
{
    public class SearchQuestionnaireManageModel
    {
        /// <summary>
        /// 问卷名称
        /// </summary>
        public string QuestionnaireName { get; set; }
        /// <summary>
        /// 问卷类型
        /// </summary>
        public Guid QuestionnaireType { get; set; }
        /// <summary>
        /// 当前状态 1-已发布 2-未发布
        /// </summary>
        public int? CurrentState { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 所属街道Id
        /// </summary>
        public Guid? StreetId { get; set; }
        /// <summary>
        /// 所属驿站Id
        /// </summary>
        public Guid? PostStationId { get; set; }
    }
}
