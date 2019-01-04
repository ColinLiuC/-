

using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.Service
{
    public sealed class MapOverlaysService : EfCoreRepositoryBase<MapOverlays, MapOverlaysDto, Guid>, IMapOverlaysService
    {

     
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetStreetMaps(string street, string jobcenter, string juwei, string flg)
        {
            var sqlget = "";
            if (!String.IsNullOrEmpty(street) && street != "0")
            {

                sqlget += string.Format("select StreetName  from [T_Street] where Id='{0}'", street);

            }
            else
            {
                sqlget += string.Format("select StreetName  from [T_Street] ");
            }
            var cfg = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "configuration.json", ReloadOnChange = true }).Build();
            var connStr = cfg.GetSection("connStr");

            DataSet st = DBHelper.ExecuteQuery(sqlget, connStr.Value);

            if (st != null)
            {
                var sqlgetLonLat = "";
       
             
                if (st.Tables.Count > 0 && st.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in st.Tables[0].Rows)
                    {
                        sqlgetLonLat += string.Format("  select [Name],[Longitude],[Latitude],[Type] from [T_MapOverlays] where '{0}' like '%'+Name+'%' and [Type]='1' ", row["StreetName"]);
                    }
                }

                
                DataSet std = DBHelper.ExecuteQuery(sqlgetLonLat, connStr.Value);
                var juweiStyle = "";
                if (flg == "1")
                {
                    juweiStyle = " strokeColor: '#edf0f2', strokeWeight: 4, strokeOpacity: 0, fillColor: '#{0}', fillOpacity: 1 ";

                }
                else
                {
                    juweiStyle = " strokeColor: '#edf0f2', strokeWeight: 4, strokeOpacity: 0, fillColor: '#2c6f2c', fillOpacity: 1 ";
                }

                var streetTemplate = "var {0}=new BMap.Polygon([{1}],{2});map.addOverlay({0});{0}.addEventListener('click', ClickMapDistrict('{3}'));";
                var juweiTemplate = "var {0}=new BMap.Polygon([{1}],{2});map.addOverlay({0});{0}.addEventListener('click', ClickMapDistrict('{3}'));";
                string identify = "";
                string points = "", all = "";
                if (std != null)
                {
                    for (int j = 0; j < std.Tables.Count; j++)
                    {


                        for (int i = 0; i < std.Tables[j].Rows.Count; i++)
                        {
                            if ((std.Tables[j].Rows[i]["Name"].ToString() + std.Tables[j].Rows[i]["Type"].ToString()) != identify && identify != "")
                            {
                                var lastname = identify.Substring(0, identify.Length - 1);
                                var lasttype = identify.Substring(identify.Length - 1);

                                all += string.Format(juweiTemplate, lastname, points, "{" + string.Format(juweiStyle, "5ec2f1") + "}", std.Tables[j].Rows[i]["Name"].ToString());

                                identify = std.Tables[j].Rows[i]["Name"].ToString() + std.Tables[j].Rows[i]["Type"].ToString();
                                points = "";
                                points += string.Format("new BMap.Point({0}, {1}),", std.Tables[j].Rows[i]["Longitude"].ToString(), std.Tables[j].Rows[i]["Latitude"].ToString());
                            }
                            else
                            {
                                identify = std.Tables[j].Rows[i]["Name"].ToString() + std.Tables[j].Rows[i]["Type"].ToString();
                                points += string.Format("new BMap.Point({0}, {1}),", std.Tables[j].Rows[i]["Longitude"].ToString(), std.Tables[j].Rows[i]["Latitude"].ToString());
                            }
                            if (i == std.Tables[j].Rows.Count - 1)
                            {
                                var lastname = identify.Substring(0, identify.Length - 1);
                                var lasttype = identify.Substring(identify.Length - 1);

                                all += string.Format(juweiTemplate, lastname, points, "{" + string.Format(juweiStyle, "5ec2f1") + "}", std.Tables[j].Rows[i]["Name"].ToString());


                                identify = std.Tables[j].Rows[i]["Name"].ToString() + std.Tables[j].Rows[i]["Type"].ToString();
                                points = "";
                            }
                        }
                    }
                }

                return all;


            }


            return "";


        }

      


    }
}
