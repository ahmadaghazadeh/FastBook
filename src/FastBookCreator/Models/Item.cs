using System;
using System.ComponentModel.DataAnnotations;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class Item
    {


        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorLogo")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Logo")]
        public byte[] LOGO { get; set; }

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Id", SortEnabled = true)]
        public long _id { get; set; }

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "ItemTypeId", SortEnabled = true)]
        public long ITEM_TYPE_ID { get; set; }


        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "ItemTypeId", SortEnabled = true)]
        public long ITEM_TYPE_NAME { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorlessonId")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Lesson", SortEnabled = true)]
        public long LESSON_ID { get; set; }

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Page", SortEnabled = true)]
        public long PAGE_ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorItemTitle")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Name", SortEnabled = true)]
        public string ITEM_TITLE { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorContent")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Content", SortEnabled = true)]
        public string CONTENT { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorMedia")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Media", SortEnabled = true)]
        public string MEDIA { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorStoreType")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "StoreType")]
        public int STORE_TYPE { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorDescription")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Description", SortEnabled = true)]
        public string DESCRIPTION { get; set; }


    }
}