using System;
using System.ComponentModel.DataAnnotations;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class Lesson
    {
        
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorLogo")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Logo")]
        public byte[] LOGO { get; set; }

        public long RESOURCE_ID { get; set; }

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Id", SortEnabled = true)]
        public long _id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorLessonTitle")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Name", SortEnabled = true)]
        public string LESSON_TITLE { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorColor")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Color")]
        public int COLOR { get; set; }
 
    }
}