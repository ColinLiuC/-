using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
    /// <summary>
    /// 短信接口
    /// </summary>
    public interface ISmsService
    {
        bool SendCode(string mobile, string code);
        bool SendCode(string[] mobiles, string code);

        bool SendMessage(string mobile, string content);

        bool SendMessage(string[] mobiles, string content);

    }
}
