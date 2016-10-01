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
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "PackId", SortEnabled = true, FilterEnabled = true)]
        public long Id { get; set; }
        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "PackName", FilterEnabled = true)]
        public string PackName { get; set; } 
        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Description", FilterEnabled = true)]
        public string Description { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "MethodId", FilterEnabled = true)]
        public string MethodId { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "SubjectId", FilterEnabled = true)]
        public string SubjectId { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Logo", FilterEnabled = true)]
        public string Logo { get; set; }

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Color", FilterEnabled = true)]
        public string Color { get; set; }

    }
}