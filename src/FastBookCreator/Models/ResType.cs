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
        Pack=0,Method=1,Item=2, ItemDetail=3, Lesson=4,Page=5
    }
}