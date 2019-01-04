using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.Service
{
    public sealed class PeopleService : EfCoreRepositoryBase<User, UserDto, Guid>, IPeopleService
    {
        /// <summary>
        ///通过市民云Id得到活动详细信息
        ///</summary>
        public List<ActivityAndEvaluationDto> TestAsync(string username, bool IsComment)
        {
            Expression<Func<ActivityAndEvaluationDto, bool>> where = PredicateExtensions.True<ActivityAndEvaluationDto>();
            if (IsComment == true)
            {
                where = where.And(p => p.isComment == IsComment);
            }
            if (IsComment == false)
            {
                where = where.And(p => p.isComment == IsComment);
            }
            if (username != null)
            {
                where = where.And(p => p.ShiMinYunId == username);
            }

            var getactivities = _dbContext.ActivityRegisters.Join(_dbContext.Activities, p => p.ActivityId, p => p.Id, (ActivityRegisters, Activities) => new ActivityAndEvaluationDto()
            {
                ActivityId = Activities.Id,
                ActivityName = Activities.ActivityName,
                ActivityType = Activities.ActivityType,
                ActivityAddress = Activities.ActivityAddress,
                MeetingRoom = Activities.MeetingRoom,
                StartTime = Activities.StartTime,
                EndTime = Activities.EndTime,
                ActivityImg = Activities.ActivityImg,
                AttachmentPath = Activities.AttachmentPath,
                isComment=ActivityRegisters.IsComment,
                EvaluationName=ActivityRegisters.EnrolmentName,
                ShiMinYunId= ActivityRegisters.ShiMinYunId,
                Id =ActivityRegisters.Id,
                ActiveState= Activities.ActiveState,
                CreationTime= ActivityRegisters.CreationTime
            }).Where(where);

            var result = getactivities.OrderByDescending(p => p.CreationTime).ToList();
            return result;
        }

        /// <summary>
        ///通过身份证号(IdCard)和状态得到事项详细信息
        ///</summary>
        public async Task<object> GetWork(Guid Id,string StatusCode)
        {

            var getactivities = _dbContext.WorkTransact.Join(_dbContext.ResidentWork, p => p.ResidentWorkId, p => p.Id, (WorkTransact, ResidentWork) => new
            {
                WorkTransact.Id,
                WorkTransact.ResidentWorkId,//事项ID
                WorkTransact.ResidentWorkName,//事项名
                WorkTransact.CreationTime,//提交时间
                WorkTransact.StatusCode,//事项状态
                WorkTransact.IdCard,//身份证号
                WorkTransact.PeopleID
            }).Where(i => i.PeopleID == Id && i.StatusCode == StatusCode).ToList();
            return getactivities;
        }

        /// <summary>
        /// 通过市民云Id得到服务信息
        /// PJ_RegistStatus:当前状态 1：未登记   2：已登记
        /// isComment:是否评价  1：已评价   2：未评价
        /// </summary>
        public List<ReceptionServiceAndEvaluationDto> GetService(string username, int? isComment,int? PJ_RegistStatus)
        {
            Expression<Func<ReceptionServiceAndEvaluationDto, bool>> where = PredicateExtensions.True<ReceptionServiceAndEvaluationDto>();
            if (username != null)
            {
                where = where.And(p => p.ShiMinYunId == username);
            }
            if (isComment==1)
            {
                where = where.And(p => p.Type ==1);
            }
            if (isComment==2)
            {
                where = where.And(p => p.Type == 2);
            }
            if (PJ_RegistStatus!=null)
            {
                where = where.And(p => p.PJ_RegistStatus == PJ_RegistStatus);
            }
            var list = _dbContext.ReceptionServices.Join(_dbContext.ServiceApplications, p => p.Id, p => p.ServiceId, (ReceptionService, ServiceApplications) => new ReceptionServiceAndEvaluationDto()
            {
                ShiMinYunId= ServiceApplications.ShiMinYunId,
                ServiceGuid = ServiceApplications.ServiceId,
                ServiceName=ReceptionService.ServiceName,
                ServiceAddress= ReceptionService.ServiceAddress,
                TimeDescription= ReceptionService.TimeDescription,
                Type= ServiceApplications.Type,
                PJ_RegistStatus=ServiceApplications.PJ_RegistStatus,
                Evaluator=ServiceApplications.ApplicantName,
                Id=ServiceApplications.Id,
                CreationTime= ServiceApplications.CreationTime,
                AttachmentPath= ReceptionService.AttachmentPath,
                Phone=ReceptionService.Phone
            }).Where(where);

           
            var result = list.OrderByDescending(p => p.CreationTime).ToList();
            return result;
        }


        public List<ActivityRegisterAndEvaluationDto> GetActivity(string name, bool IsComment)
        {
            Expression<Func<ActivityRegisterAndEvaluationDto, bool>> where = PredicateExtensions.True<ActivityRegisterAndEvaluationDto>();
            where = where.And(p => p.EnrolmentName == name);
            if (IsComment == true)
            {
                where = where.And(p => p.EvaluationContent != "");
            }
            else
            {
                where = where.And(p => p.EvaluationContent == "");
            }
            var query = (from item1 in _dbContext.ActivityRegisters
                         join item2 in _dbContext.Activities on item1.ActivityId equals item2.Id into a
                         from item3 in a.DefaultIfEmpty()
                         join item4 in _dbContext.ActivityEvaluations on item1.ActivityId equals item4.ActivityId into b
                         from item5 in b.DefaultIfEmpty()
                         select new ActivityRegisterAndEvaluationDto()
                         {
                             ActivityId = item3.Id,
                             ActivityName = item3.ActivityName!=null? item3.ActivityName : "",
                             ActivityType = item3.ActivityType,
                             ActivityAddress = item3.ActivityAddress != null ? item3.ActivityAddress : "",
                             MeetingRoom = item3.MeetingRoom,
                             StartTime = item3.StartTime,
                             EndTime = item3.EndTime,
                             ActivityImg = item3.ActivityImg != null ? item3.ActivityImg : "",
                             AttachmentPath = item3.AttachmentPath != null ? item3.AttachmentPath : "",
                             EvaluationName = item5.EvaluationName != null ? item5.EvaluationName : "",//评价人姓名
                             EvaluationContent = item5.EvaluationContent != null ? item5.EvaluationContent : "",//评论 
                             Score = item5.Score,//满意度
                             EnrolmentName = item1.EnrolmentName != null ? item1.EnrolmentName : "",//报名人姓名,
                             Id = item1.Id,
                             ActiveState = item3.ActiveState,
                             CreationTime = item1.CreationTime
                         }).Where(where);
            var list = query.ToList();
            return list;
        }

        public List<ServiceApplicationAndEvaluationDto> GetService2()
        {
            Expression<Func<ServiceApplicationAndEvaluationDto, bool>> where = PredicateExtensions.True<ServiceApplicationAndEvaluationDto>();
            var query = (from item1 in _dbContext.ServiceApplications
                         join item2 in _dbContext.ReceptionServices on item1.ServiceId equals item2.Id into a
                         from item3 in a.DefaultIfEmpty()
                         join item4 in _dbContext.ServiceEvaluations on item1.ServiceId equals item4.ServiceGuid into b
                         from item5 in b.DefaultIfEmpty()
                         select new ServiceApplicationAndEvaluationDto()
                         {
                             ServiceId = item3.Id,
                             ServiceName = item3.ServiceName,
                             ServiceAddress = item3.ServiceAddress,
                             TimeDescription = item3.TimeDescription,
                             Evaluator = item5.Evaluator,//评价人
                             Satisfaction = item5.Satisfaction,
                             EvaluationContent = item5.EvaluationContent,
                             ApplicantName = item1.ApplicantName,//服务申请人
                             CurrentState = item1.CurrentState//当前状态
                         }).Where(where);
            return query.ToList();
        }



    }
}
