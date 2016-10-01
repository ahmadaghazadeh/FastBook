using System;
using System.ComponentModel.DataAnnotations;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class Pack
    {
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "_id", SortEnabled = true, FilterEnabled = true)]
        public long _id { get; set; }
        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "PACK_NAME", FilterEnabled = true)]
        public string PACK_NAME { get; set; } 
        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Description", FilterEnabled = true)]
        public string Description { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "METHOD_ID", FilterEnabled = true)]
        public string METHOD_ID { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "SUBJECT_ID", FilterEnabled = true)]
        public string SUBJECT_ID { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "LOGO", FilterEnabled = true)]
        public string LOGO { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "COLOR", FilterEnabled = true)]
        public string COLOR { get; set; }

    }
}