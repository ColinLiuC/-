using AutoMapper;
using CoreSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
  public class ServiceApplicationProfile : Profile, IProfile
    {
        public ServiceApplicationProfile()
        {
            CreateMap<ServiceApplication, Dto.ServiceApplicationDto>();
            CreateMap<Dto.ServiceApplicationDto, ServiceApplication>();
        }
    }
}
