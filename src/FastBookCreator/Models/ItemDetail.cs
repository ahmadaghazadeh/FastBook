using System;
using System.ComponentModel.DataAnnotations;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class ItemDetail
    {
        public long _id { get; set; }

        public long ITEM_ID { get; set; }
        public string ANSWER { get; set; }
        
        public bool IS_ANSWER { get; set; }

        public long ORDER_NUMBER { get; set; }
    }
}