using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.WorkTransact
{
    public static class HelperWorkTransact
    {

        /// <summary>
        /// 计算当前状态
        /// </summary>
        /// <param name="lasttime">最晚处理日期</param>
        /// <returns></returns>
        public static string GetStatusNow(DateTime? lasttime)
        {
            try
            {
                var time = DateTime.Parse(lasttime.Value.ToString("yyyy-MM-dd")) - DateTime.Now;
                int _time = time.Days;
                if (_time <= 3)
                    return "red";
                else if (_time > 3 && _time <= 5)
                    return "yellow";
                else
                    return "green";
            }
            catch (Exception)
            {
                return "green";
            }

        }

    }
}
