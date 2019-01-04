using CoreSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto.MyModel
{
   public class MyWorkTransact
    {
        public WorkTransact workTransact;
        public ResidentWork residentWork;
        public int residentWorkType;
        public string Deadline;
        public string residentWorkType_ds;
        public DateTime? lastDeadline;
        public string statusNow;
    }
}
