using System.ComponentModel.DataAnnotations;

namespace PROGETTO_SETTIMINALE_BE_S2_L5__Vescio_Pia_Francesca.Models
{
    public class ProductInputModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Il campo {0} è obbligatorio. Inseriscilo correttamente.")]
        public string Name { get; set; }

        [Display(Name = "Descrizione")]
        [Required(ErrorMessage = "Il campo {0} è obbligatorio. Inseriscilo correttamente.")]
        public string Description { get; set; }

        [Display(Name = "Prezzo")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public int Price { get; set; } = 0;

        [Display(Name = "Immagine principale")]
        [Required(ErrorMessage = "Il campo {0} è obbligatorio. Inseriscilo correttamente.")]
        public IFormFile Cover { get; set; }
        [Display(Name = "Immagine secondaria")]
        [Required(ErrorMessage = "Il campo {0} è obbligatorio. Inseriscilo correttamente.")]
        public IFormFile AdditionalImage1 { get; set; }
        [Display(Name = "Immagine secondaria")]
        [Required(ErrorMessage = "Il campo {0} è obbligatorio. Inseriscilo correttamente.")]
        public IFormFile AdditionalImage2 { get; set; }
    }
}
