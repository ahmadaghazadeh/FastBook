using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FastBookCreator.Entities
{
    public class Item
    {
        [Key]
        public string id { get; set; }

        [Required]
        public string ItemTypeId { get; set; }

        [Required]
        public string PageId { get; set; }

        [Required]
        public string LessonId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}