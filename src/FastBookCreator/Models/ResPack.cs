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

        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "ResourceID", SortEnabled = true)]
        public long _id { get; set; }

      
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "Image", SortEnabled = true)]
        public Byte[] DATA { get; set; }

        
     
        [GridColumnDisplay(ResourceType = typeof(ResPack), Name = "Name", SortEnabled = true)]
        public string NAME { get; set; }
 
    }
}