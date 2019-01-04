using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto.MyModel
{
    public class MyKeyAndValue
    {
        public Guid? Key;
        public string KeyName;
        public int Count;
    }

    public class MyJuWeiDituModel
    {
        //public Guid Key;
        public double? Lng;//纬度
        public double? Lat;//经度
        public int Count;//总人数
    }
}
