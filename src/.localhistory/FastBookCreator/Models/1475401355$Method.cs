﻿using System;
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
        public string Logo { get; set; }

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "MethodID", SortEnabled = true)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorMethodName")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "MethodName", SortEnabled = true)]
        public string MethodName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorRoomCount")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "RoomCount", SortEnabled = true)]
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