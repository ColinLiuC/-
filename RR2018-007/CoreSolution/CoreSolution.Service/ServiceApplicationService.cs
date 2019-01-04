using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Tools;
using System.Data;
using System.Linq;

namespace CoreSolution.Service
{
    public class ServiceApplicationService : EfCoreRepositoryBase<ServiceApplication, ServiceApplicationDto, Guid>, IServiceApplication
    {
        public ApplicationInformationDto GetApplicationInformationDataById(Guid id)
        {
            var applications = _dbContext.ServiceApplications;
            var receptions = _dbContext.ReceptionServices;
            var list = applications.Join(receptions, i => i.ServiceId, i => i.Id, (application, reception) => new ApplicationInformationDto
            {
                Id = application.Id,
                ApplicantName = application.ApplicantName,
                ContactNumber = application.ContactNumber,
                ServiceName = reception.ServiceName,
                Category = reception.Category
            });
           ApplicationInformationDto  dto=  list.Where(i => i.Id == id).First();
            return dto;
        }


        public IList<ApplicationInformationDto> GetServiceResult(ServiceResultDto srt,int pageIndex,int pageSize,out int total)
        {           
            var applications = _dbContext.ServiceApplications;
            var receptions = _dbContext.ReceptionServices;
            var list=  applications.Join(receptions, i => i.ServiceId, i => i.Id, (application, reception) => new ApplicationInformationDto
            {
                Id = application.Id,
                ApplicantName = application.ApplicantName,
                ContactNumber = application.ContactNumber,
                ServiceName = reception.ServiceName,
                Category = reception.Category,
                RegisterDate = application.RegisterDate,
                CurrentState = application.CurrentState,
                PJ_RegistStatus = application.PJ_RegistStatus,
                ApplicationSource=application.ApplicationSource,
                StreetId=reception.StreetId,
                PostStationId=reception.PostStationId
            });
            var where = PredicateExtensions.True<ApplicationInformationDto>();

            if (!string.IsNullOrWhiteSpace(srt.ApplicantName))
            {
                where = where.And(i => i.ApplicantName.Contains(srt.ApplicantName));
            }
            if (!string.IsNullOrWhiteSpace(srt.ServiceName))
            {
                where = where.And(i=>i.ServiceName.Contains(srt.ServiceName));
            }
            if (srt.ServiceCategory != 0 && srt.ServiceCategory != null)
            {
                where = where.And(i=>i.Category==srt.ServiceCategory);
            }
            if (srt.CurrentState != 0 && srt.CurrentState != null)
            {
                where = where.And(i => i.CurrentState == srt.CurrentState);
            }
            if (srt.PJ_RegistStatus != 0 && srt.PJ_RegistStatus != null)
            {
                where = where.And(i => i.PJ_RegistStatus == srt.PJ_RegistStatus);
            }
            if (srt.startTime != null && srt.endTime != null)
            {
                where = where.And(i=>i.RegisterDate>=srt.startTime&&i.RegisterDate<=srt.endTime);
            }
            if (srt.StreetId!=null&& srt.StreetId!=default(Guid)) {
                 where = where.And(i => i.StreetId == srt.StreetId);
            }
            if (srt.PostStationId != null && srt.PostStationId != default(Guid))
            {
                where = where.And(i => i.PostStationId == srt.PostStationId);
            }
            if (srt.ApplicationType==1)
            {
                where = where.And(i => i.ApplicationSource == 1);
            }
            if (srt.ApplicationType==2)
            {
                if (srt.ApplicationSource==0)
                {
                    where = where.And(i => i.ApplicationSource == srt.ApplicationSource);
                }
            }
            total= list.Where(where).Count();
            var result = list.Where(where).OrderBy(i=>i.CurrentState).ThenByDescending(i => i.RegisterDate).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return result;           
        }
        
        public int GetCount (Guid? streetId, Guid? postStationId, int applicationSource)
        {
            var applications = _dbContext.ServiceApplications;
            var receptions = _dbContext.ReceptionServices;
            var list = applications.Join(receptions, i => i.ServiceId, i => i.Id, (application, reception) => new ApplicationServiceDto
            {
                serviceId = reception.Id,
                streetId = reception.StreetId,
                postStationId = reception.PostStationId,
                CurrentState=application.CurrentState,
                ApplicationSource=application.ApplicationSource
            });
            var where = PredicateExtensions.True<ApplicationServiceDto>();
            if (streetId!=null)
            {
                where = where.And(p => p.streetId == streetId);
            }
            if (postStationId != null)
            {
                where = where.And(p => p.postStationId == postStationId);
            }
            where = where.And(p=>p.ApplicationSource== applicationSource);
            return  list.Where(where).Count();
        }

        public int GetApplicationCount(Guid? streetId, Guid? postStationId)
        {
            var applications = _dbContext.ServiceApplications;
            var receptions = _dbContext.ReceptionServices;
            var list = applications.Join(receptions, i => i.ServiceId, i => i.Id, (application, reception) => new ApplicationServiceDto
            {
                serviceId = reception.Id,
                streetId = reception.StreetId,
                postStationId = reception.PostStationId,
                CurrentState = application.CurrentState,
                ApplicationSource = application.ApplicationSource
            });
            var where = PredicateExtensions.True<ApplicationServiceDto>();
            if (streetId != null)
            {
                where = where.And(p => p.streetId == streetId);
            }
            if (postStationId != null)
            {
                where = where.And(p => p.postStationId == postStationId);
            }
            where = where.And(p => p.CurrentState == 1);
            return list.Where(where).Count();
        }

        public int GetServiceResultCount(Guid? streetId, Guid? postStationId)
        {
            var applications = _dbContext.ServiceApplications;
            var receptions = _dbContext.ReceptionServices;
            var list = applications.Join(receptions, i => i.ServiceId, i => i.Id, (application, reception) => new ApplicationServiceDto
            {
                serviceId = reception.Id,
                streetId = reception.StreetId,
                postStationId = reception.PostStationId,
                PJ_RegistStatus=application.PJ_RegistStatus
            });
            var where = PredicateExtensions.True<ApplicationServiceDto>();
            if (streetId != null)
            {
                where = where.And(p => p.streetId == streetId);
            }
            if (postStationId != null)
            {
                where = where.And(p => p.postStationId == postStationId);
            }
            where = where.And(p => p.PJ_RegistStatus == 1);
            return list.Where(where).Count();
        }

        public Guid GetServiceType(Guid? streetId, Guid? postStationId,int applicationCource)
        {
            var applications = _dbContext.ServiceApplications;
            var receptions = _dbContext.ReceptionServices;
            var list = applications.Join(receptions, i => i.ServiceId, i => i.Id, (application, reception) => new ApplicationServiceDto
            {
                serviceId = reception.Id,
                streetId = reception.StreetId,
                postStationId = reception.PostStationId,
                Category=reception.Category,
                ApplicationSource=application.ApplicationSource,
                RegisterDate = application.RegisterDate,
                ServiceName=reception.ServiceName,
            });
            var where = PredicateExtensions.True<ApplicationServiceDto>();
            if (streetId != null)
            {
                where = where.And(p => p.streetId == streetId);
            }
            if (postStationId != null)
            {
                where = where.And(p => p.postStationId == postStationId);
            }
            where = where.And(p => p.ApplicationSource == applicationCource);
            where = where.And(p =>p.RegisterDate.Value.ToString("yyyy-MM-dd") ==DateTime.Now.ToString("yyyy-MM-dd"));
            var list1 = list.Where(where);
            if (list1.Count()>0)
            {
                var dto = list1.GroupBy(p => p.serviceId).OrderByDescending(p => p.Count()).FirstOrDefault();
                if (dto != null)
                {
                    return dto.Key;
                }
                else
                {
                    return default(Guid);
                }              
            }
            else
            {
                return default(Guid);
            }              
        }
    }
}
