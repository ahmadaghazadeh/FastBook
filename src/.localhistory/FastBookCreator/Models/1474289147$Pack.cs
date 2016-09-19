using System;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class Foo
    {
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "FooTitle", SortEnabled = true, FilterEnabled = true)]
        public string Name { get; set; }
        
        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "FooEnable", FilterEnabled = true)]
        public bool Enabled { get; set; } = true;

        [GridColumnDisplay(ResourceType = typeof(Resource), Name = "FooDate", Format = "{0:yyyy/MM/dd HH:mm:ss}", FilterEnabled = true)]
        public DateTime FooDate { get; set; } = DateTime.Now;

    }
}