using GridMvc.DataAnnotations;
 

namespace FastBookCreator.Models
{
    public class ResType
    {
        public long _id { get; set; }
        
        public ResTypeID typeId { get; set; }
    }

    public enum ResTypeID
    {
        Common=0, PACK_IMAGE = 1,PACK_SOUND=2
    }
}