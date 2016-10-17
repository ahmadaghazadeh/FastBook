using System;
using System.ComponentModel.DataAnnotations;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class Page
    {


        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorPageTypeId")]
        public long PAGE_TYPE_ID { get; set; }

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "PageType", SortEnabled = true)]
        public string PAGE_TYPE_NAME { get; set; }


        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Id", SortEnabled = true)]
        public long _id { get; set; }

 
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "LessonNumber", SortEnabled = true)]
        public long LESSON_ID { get; set; }

 
 
    }
}