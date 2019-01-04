using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
   public class QuestionnaireManage: EntityBaseFull
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
        /// 当前状态
        /// </summary>
        public int CurrentState { get; set; }
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
