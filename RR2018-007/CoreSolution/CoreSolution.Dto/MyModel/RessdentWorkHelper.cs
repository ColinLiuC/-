using CoreSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto.MyModel
{
    public class RessdentWorkHelper : IEqualityComparer<MyResidentWork>
    {
        /// <summary>
        /// 重写
        /// 判断街道内事项id是否有多条，有则保留一条数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(MyResidentWork x, MyResidentWork y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;
            return x.residentWork_Attach.ResidentWorkId == y.residentWork_Attach.ResidentWorkId;
        }

        public int GetHashCode(MyResidentWork student)
        {
            return student.ToString().GetHashCode();
        }
    }
}
