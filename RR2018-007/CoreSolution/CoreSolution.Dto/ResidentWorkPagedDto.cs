using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{
    public class ResidentWorkPagedDto
    {
        public int Count { get; set; }
        public IList<ResidentWorkListDto> Data { get; set; }
        public class ResidentWorkListDto : EntityDto<Guid>
        {
            public string ResidentWorkName { get; set; }
            public ResidentWorkType ResiWorkType { get; set; }
        }
    }
}
