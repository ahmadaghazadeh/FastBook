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
        
        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorLogo")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "Logo")]
        public byte[] Logo { get; set; }

        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "PackId", SortEnabled = true)]
        public long _id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorPackName")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "PackName", SortEnabled = true)]
        public string PackName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorMethodId")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "MethodId", SortEnabled = true)]
        public string MethodId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorSubjectId")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "SubjectId", SortEnabled = true)]
        public string SubjectId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorColor")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "Color")]
        public string Color { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorDescription")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "Description", SortEnabled = true)]
        public string Description { get; set; }
    }
}