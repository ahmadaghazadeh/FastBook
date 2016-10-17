using System;
using System.ComponentModel.DataAnnotations;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class Method
    {
        
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorLogo")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Logo")]
        public byte[] LOGO { get; set; }

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "MethodID", SortEnabled = true)]
        public long _id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorMethodName")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "MethodName", SortEnabled = true)]
        public string METHOD_NAME { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorRoomCount")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "RoomCount", SortEnabled = true)]
        public string ROOM_COUNT { get; set; }

     
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "AllowEdit", SortEnabled = true)]
        public string ALLOW_EDIT { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorColor")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Color")]
        public string COLOR { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorLanguage")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Language", SortEnabled = true)]
        public string LANGUAGE { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorDescription")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Description", SortEnabled = true)]
        public string DESCRIPTION { get; set; }
    }
}