using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{
    public class ResidentWorkDto : EntityBaseFullDto
    {
        public string ResidentWorkName { get; set; }
        public int ResidentWorkType { get; set; }
        public string ResidentWorkType_ds { get; set; }

        public string ResidentWorkFlow { get; set; }
        public string ResidentWorkFlowImg { get; set; }
        public string ResidentWorkFlowImgPaths { get; set; }

        public string RelevantPolicies { get; set; }
        public string AdministrativeBasis { get; set; }
        public string Requirement { get; set; }
        public string Material { get; set; }
        public float Charge { get; set; }
        public string Deadline { get; set; }
        public bool IsPublish { get; set; }
        public string IsPublish_ds { get; set; }


        public bool IsGuiDang { get; set; } = false;

        public string GuiDangRenark { get; set; }
        public DateTime? GuiDangTime { get; set; }
        public string GuiDangUser { get; set; }

        public Guid StreetId { get; set; }

        public string StationIds { get; set; }
        public string StationNames { get; set; }

        
    }
}
