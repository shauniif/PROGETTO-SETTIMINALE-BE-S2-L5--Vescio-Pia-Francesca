using System.ComponentModel.DataAnnotations;

namespace PROGETTO_SETTIMINALE_BE_S2_L5__Vescio_Pia_Francesca.Entities
{
    public class Product : BaseEntity
    {
        
        public string Name { get; set; }
      
        public string Description { get; set; }
      

        public int Price { get; set; } = 0;
       
        public IFormFile Cover { get; set; }
       
        public IFormFile AdditionalImage1 { get; set; }

        public IFormFile AdditionalImage2 { get; set; }

       
    }
}
