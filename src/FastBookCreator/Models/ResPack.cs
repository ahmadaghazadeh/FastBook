using System;
using System.ComponentModel.DataAnnotations;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class ResPack
    {

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "ResourceID", SortEnabled = true)]
        public long _id { get; set; }

      
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Image")]
        public byte[] DATA { get; set; }

        
     
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "Name", SortEnabled = true,FilterEnabled = true)]
        public string NAME { get; set; }
 
    }
}