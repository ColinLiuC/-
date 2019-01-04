using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.QuestionBankManage
{
    public class SearchQuestionBankManageModel
    {
        /// <summary>
        /// 题目类型 1-单选题 2-多选题 3-问答题
        /// </summary>
        public int? TitleType { get; set; }
        /// <summary>
        /// 题目标题
        /// </summary>
        public string TitleName { get; set; }
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
