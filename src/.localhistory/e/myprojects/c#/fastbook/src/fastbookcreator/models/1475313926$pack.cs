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
        public long Id { get; set; }
        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "PACK_NAME", FilterEnabled = true)]
        public string PackName { get; set; } 
        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Description", FilterEnabled = true)]
        public string Description { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "METHOD_ID", FilterEnabled = true)]
        public string MethodId { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "SUBJECT_ID", FilterEnabled = true)]
        public string SubjectId { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "LOGO", FilterEnabled = true)]
        public string Logo { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "COLOR", FilterEnabled = true)]
        public string Color { get; set; }

    }
}