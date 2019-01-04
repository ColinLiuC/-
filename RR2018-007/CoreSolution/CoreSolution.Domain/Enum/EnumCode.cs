using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoreSolution.Domain.Enum
{
    public class EnumCode
    {
        /// <summary>
        /// 是否发布
        /// </summary>
        public enum IsYesOrNo
        {
            [Description("是")]
            Yes = 1,
            [Description("否")]
            No = 2
        }
        /// <summary>
        /// 所属分类
        /// </summary>
        public enum ServiceCateGory
        {
            /// <summary>
            /// 助餐
            /// </summary>
            [Description("助餐")]
            助餐 = 1,
            /// <summary>
            /// 助洁
            /// </summary>
            [Description("助洁")]
            助洁 = 2,
            /// <summary>
            /// 家电维修
            /// </summary>
            [Description("家电维修")]
            家电维修 = 3,

            /// <summary>
            /// 医疗保健
            /// </summary>
            [Description("医疗保健")]
            医疗保健 = 4,

        }
        /// <summary>
        /// 服务收费
        /// </summary>
        public enum ServiceCharge
        {
            [Description("已收费")]
            已收费 = 1,
            [Description("未收费")]
            未收费 = 2
        }

        /// <summary>
        /// 活动类型
        /// </summary>
        public enum ActivityTypeEnum
        {
            [Description("线上")]
            线上 = 1,
            [Description("线下")]
            线下 = 2
        }

        /// <summary>
        /// 事项分类
        /// </summary>
        public enum ResidentWorkType
        {
            [Description("房屋事务")]
            fangwushiwu = 1,
            [Description("上线服务")]
            shangxianfuwu = 2,
            [Description("社保事务")]
            shebaoshiwu = 3,
            [Description("人口管理")]
            renkouguabli = 4,
            [Description("工会事务")]
            gonghuishiwu = 5,
        }
        /// <summary>
        /// 公告通知发布渠道
        /// </summary>
        public enum NoticeChannel
        {
            [Description("全部")]
            quanbu = 0,
            [Description("虹口市民驿站综合服务平台")]
            fuwupingtai = 1,
            [Description("市民驿站APP")]
            shiminapp = 2

        }

        /// <summary>
        /// 公告通知状态
        /// </summary>
        public enum NoticeState
        {
            [Description("已发布")]
            yifabu = 0,
            [Description("未发布")]
            weifabu = 1

        }


        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            [Description("男")]
            nan = 0,
            [Description("女")]
            nv = 1,
            [Description("全部")]
            all = 2
        }

        /// <summary>
        /// 婚姻状态
        /// </summary>
        public enum Marriage
        {
            [Description("已婚")]
            yi = 0,
            [Description("未婚")]
            wei = 1
        }
        /// <summary>
        /// 残疾级别
        /// </summary>
        public enum Level
        {
            [Description("一级")]
            yi = 1,
            [Description("二级")]
            er = 2,
            [Description("三级")]
            san = 3,
            [Description("四级")]
            si = 4
        }
        /// <summary>
        /// 就业情况
        /// </summary>
        public enum Employment
        {
            [Description("就业")]
            you = 0,
            [Description("无业")]
            wu = 1,
        }


        public enum StatusCode
        {
            No = 0,
            Yes = 1
        }


        public enum ProStatusCode
        {
            [Description("已处理")]
            Yes = 0,
            [Description("未处理")]
            No = 1,
        }

        /// <summary>
        ///  排班管理 分组
        /// </summary>
        public enum GroupCateGory
        {
            [Description("第一组")]
            Zu1 = 1,
            [Description("第二组")]
            Zu2 = 2,
            [Description("第三组")]
            Zu3 = 3,
            [Description("第四组")]
            Zu4 = 4
        }

        /// <summary>
        ///  经费使用登记管理 经费类型
        /// </summary>
        public enum ExpenditureCateGory
        {
            [Description("物品维修")]
            A = 1,
            [Description("购买物资")]
            B = 2,
            [Description("其他")]
            C = 3,
        }

        /// <summary>
        ///  固定资产 目前情况
        /// </summary>
        public enum FixedAssetsCurrentState
        {
            [Description("空闲")]
            KongXian = 1,
            [Description("正在使用")]
            ShiYong = 2,
            [Description("正在维修")]
            WeiXiu = 3,
            [Description("已报废")]
            BaoFei = 4
        }

        /// <summary>
        ///  使用情况登记 当前状态
        /// </summary>
        public enum UseCurrentState
        {
            [Description("已归还")]
            YiGuiHuan = 1,
            [Description("未归还")]
            WeiGuiHuan = 2
        }

        /// <summary>
        ///  维修及报废登记 当前状况
        /// </summary>
        public enum WeiXiuCurrentState
        {
            [Description("正在维修")]
            ZaiWeiXiu = 1,
            [Description("已维修完成")]
            YiWanCheng = 2,
            [Description("已报废")]
            YiBaoFei = 3
        }

        /// <summary>
        ///  维修及报废登记 类型
        /// </summary>
        public enum WeiXiuJiBaoFei
        {
            [Description("维修")]
            WeiXiu = 1,
            [Description("报废")]
            BaoFei = 2
        }
        /// <summary>
        /// 接待服务    类型
        /// </summary>
        public enum ReceptionServiceCatagory
        {
            [Description("党群服务")]
            DangQun = 1,
            [Description("统战服务")]
            TongZhan = 2,
            [Description("养老服务")]
            YangLao = 3,
            [Description("双拥服务")]
            ShuangYong = 4,
            [Description("医疗卫生")]
            YiLiao = 5,
            [Description("法律服务")]
            FaLu = 6,
            [Description("文化休闲")]
            WenHua = 7,
            [Description("白领服务")]
            BaiLing = 8,
            [Description("创业服务")]
            ChuangYe = 9,
            [Description("就业服务")]
            JiuYe = 10
        }

        public enum ServiceResoure
        {
            [Description("驿站登记")]
            DengJi = 0,
            [Description("网上申请")]
            WangShang = 1,
            [Description("未知来源")]
            WeiZhi = 2,
           
        }
    }
}
