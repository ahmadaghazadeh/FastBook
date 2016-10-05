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
        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Logo")]
        public string Logo { get; set; }

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "PackId", SortEnabled = true)]
        public long Id { get; set; }
        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "PackName", SortEnabled = true)]
        public string PackName { get; set; } 

        [Required]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "MethodId", SortEnabled = true)]
        public string MethodId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorSubjectId")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "SubjectId", SortEnabled = true)]
        public string SubjectId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorColor")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Color")]
        public string Color { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorDescription")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Description", SortEnabled = true)]
        public string Description { get; set; }
    }
}