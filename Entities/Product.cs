using System.ComponentModel.DataAnnotations;

namespace PROGETTO_SETTIMINALE_BE_S2_L5__Vescio_Pia_Francesca.Entities
{
    public class Product : BaseEntity
    {
        
        public string Name { get; set; }
      
        public string Description { get; set; }
      

        public int Price { get; set; } = 0;
       
       

       
    }
}
