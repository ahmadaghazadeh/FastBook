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
        
        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorLogo")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "Logo")]
        public byte[] LOGO { get; set; }

        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "MethodID", SortEnabled = true)]
        public long _id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorMethodName")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "MethodName", SortEnabled = true)]
        public string METHOD_NAME { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorRoomCount")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "RoomCount", SortEnabled = true)]
        public string ROOM_COUNT { get; set; }

     
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "AllowEdit", SortEnabled = true)]
        public string ALLOW_EDIT { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorColor")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "Color")]
        public string COLOR { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorLanguage")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "Language", SortEnabled = true)]
        public string LANGUAGE { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorDescription")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "Description", SortEnabled = true)]
        public string DESCRIPTION { get; set; }
    }
}