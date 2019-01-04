using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.WorkPerson;
using CoreSolution.WebApi.Models.WorkTransact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class WorkPersonPeofile : Profile, IProfile
    {
        public WorkPersonPeofile()
        {
            CreateMap<WorkPersonDto, InputWorkPersonModel>();
            CreateMap<WorkPersonDto, OutputWorkPersonModel>();
        }
    }



}
