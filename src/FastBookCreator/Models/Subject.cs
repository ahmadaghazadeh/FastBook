﻿using System;
using System.ComponentModel.DataAnnotations;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class Subject
    {
        
        

        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "SubjectID", SortEnabled = true)]
        public long _id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorSubjectName")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "SubjectName", SortEnabled = true)]
        public string SUBJECT_NAME { get; set; }

        
     
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "AllowEdit", SortEnabled = true)]
        public string ALLOW_EDIT { get; set; }
 

        [Required(ErrorMessageResourceType = typeof(ResPack), ErrorMessageResourceName = "ErrorLanguage")]
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "Language", SortEnabled = true)]
        public string LANGUAGE { get; set; }
    }
}