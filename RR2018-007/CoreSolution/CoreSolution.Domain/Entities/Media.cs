using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities.Base;


namespace CoreSolution.Domain.Entities
{

    public class Media : EntityBaseFull
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
