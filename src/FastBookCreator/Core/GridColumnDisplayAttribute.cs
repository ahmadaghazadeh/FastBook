using System;
using GridMvc.DataAnnotations;

namespace FastBookCreator.Core
{
    public class GridColumnDisplayAttribute : GridColumnAttribute
    {
        public Type ResourceType { get; set; }

        public string Name
        {
            get { return Title; }
            set
            {
                if (ResourceType == null) return;
                var prop = ResourceType.GetProperty(value);
                Title = prop?.GetValue(null)?.ToString() ?? value;
            }
            
        }
    }
}