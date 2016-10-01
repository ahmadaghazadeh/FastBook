using System;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class Pack
    {
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "PackId", SortEnabled = true, FilterEnabled = true)]
        public long PackId { get; set; }
        
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "PackName", FilterEnabled = true)]
        public string PackName { get; set; } 

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Description", FilterEnabled = true)]
        public string Description { get; set; } 

    }
}