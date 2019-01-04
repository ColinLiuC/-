using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.Extensions.Options;
using CoreSolution.Tools.WebResult;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using CoreSolution.WebApi.Models.MediaCenter;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]

    [Produces("application/json")]
    [Route("api/MediaCenter")]
    public class MediaCenterController : Controller
    {
      
        private IHostingEnvironment hostingEnv;

       
        public MediaCenterController(IHostingEnvironment env)
        {
            
            this.hostingEnv = env;
        }

        [Route("PostFile")]
        [HttpPost]
        public IActionResult PostFile(IFormFileCollection fileT)
        {
            
            var fileTypes = ConfigHelper.GetSectionValue("Uploadpictype");
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);

            //size > 1000MB refuse upload !
            if (size > 1048576000)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "大小超过限制");
            }

            //List<string> filePathResultList = new List<string>();
            //List<string> FileNameResultList = new List<string>();
            string filePathResultList = "";
            string fileNameResultList = "";


            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                if (fileNameResultList != "")
                {
                    fileNameResultList += ",";
                }

                fileNameResultList+= fileName;

                string filePath = hostingEnv.WebRootPath + $@"\Files\";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string suffix = fileName.Split('.')[1];


                if (!fileTypes.Contains(suffix.ToLower()))
                {
                    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "问件类型不对");
                }

                fileName = Guid.NewGuid() + "." + fileName.Split('.')[1];

                string fileFullName = filePath + fileName;

                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                //filePathResultList.Add($"/Files/{fileName}");
                if (filePathResultList != "")
                {
                    filePathResultList += ",";
                }

                filePathResultList += $"/Files/{fileName}";
            }

            FileReturn fileRturn = new FileReturn() { FileNames= fileNameResultList, FileUrls = filePathResultList };

            string message = $"{files.Count} file(s) /{size} bytes uploaded successfully!";

            return AjaxHelper.JsonResult(HttpStatusCode.OK, message, fileRturn);
        }

    }
    


    }
