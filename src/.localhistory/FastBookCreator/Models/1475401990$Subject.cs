using System;
using System.ComponentModel.DataAnnotations;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class Subject
    {
        
        

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "SubjectID", SortEnabled = true)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorSubjectName")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "SubjectName", SortEnabled = true)]
        public string SubjectName { get; set; }

        
     
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "AllowEdit", SortEnabled = true)]
        public string AllowEdit { get; set; }
 

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ErrorDescription")]
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Description", SortEnabled = true)]
        public string Description { get; set; }
    }
}