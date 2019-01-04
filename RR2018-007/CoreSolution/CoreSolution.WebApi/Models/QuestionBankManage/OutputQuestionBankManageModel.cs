using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.QuestionBankManage
{
    public class OutputQuestionBankManageModel: EntityBaseFullDto
    {
        /// <summary>
        /// 题目类型 1-单选题 2-多选题 3-问答题
        /// </summary>
        public int TitleType { get; set; }
        /// <summary>
        /// 题目标题
        /// </summary>
        public string TitleName { get; set; }
        /// <summary>
        /// 选择题-题目选项
        /// </summary>
        public string TitleOptions { get; set; }
        /// <summary>
        /// 题目备注
        /// </summary>
        public string TitleRemarks { get; set; }
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
