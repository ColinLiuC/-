using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.IService
{
    /// <summary>
    ///读取地图
    /// </summary>   
    public interface IMapOverlaysService : IEfCoreRepository<MapOverlays, MapOverlaysDto>, IServiceSupport
    {



        string GetStreetMaps(string street, string jobcenter, string juwei, string flg);




    }
}
