using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public class AnswerDto
    {
        public string titleName { get; set; }
        public List<AnswerContent> answerContent{ get; set; }
    }
    public class AnswerContent
    {
        public string optionName { get; set; }
        public int count { get; set; }
    }
}
