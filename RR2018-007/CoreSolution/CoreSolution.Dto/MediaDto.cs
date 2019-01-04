using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{


    public class MediaDto : EntityBaseFullDto
    {
        public string FileName { get; set; }
        public string ExtensionName { get; set; }
        public string FilePath { get; set; }
        public string ThumbnailPath { get; set; }
        public Guid MediaType { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public bool IsPublic { get; set; }

    }
}
