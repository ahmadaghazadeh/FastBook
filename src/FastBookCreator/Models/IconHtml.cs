using System;
using System.ComponentModel.DataAnnotations;
using FastBookCreator.Core;
using GridMvc.DataAnnotations;
using Resources;

namespace FastBookCreator.Models
{
    
    public class IconHtml
    {
        public string Name { get; set; }
       
        public string Icon { get; set; }

        public int ItemTypeId { get; set; }
    }
}