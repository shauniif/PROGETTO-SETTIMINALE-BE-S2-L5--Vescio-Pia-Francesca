using PROGETTO_SETTIMINALE_BE_S2_L5__Vescio_Pia_Francesca.Entities;
namespace PROGETTO_SETTIMINALE_BE_S2_L5__Vescio_Pia_Francesca
{
    public interface IProductService
    {   
        IEnumerable<Product> GetAllProducts();
        void Create(Product product);

        void Delete(int productId);
        
        Product GetById(int productId);

    }
}
