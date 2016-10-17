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
        
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorLogo")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Logo")]
        public byte[] LOGO { get; set; }

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Id", SortEnabled = true)]
        public long _id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorPackName")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "PackName", SortEnabled = true)]
        public string PACK_NAME { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorMethodId")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "MethodId", SortEnabled = true)]
        public string METHOD_ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorSubjectId")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "SubjectId", SortEnabled = true)]
        public string SUBJECT_ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorColor")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Color")]
        public int COLOR { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorDescription")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Description", SortEnabled = true)]
        public string DESCRIPTION { get; set; }
    }
}