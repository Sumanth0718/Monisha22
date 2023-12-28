using ProductAPI.Model;

namespace ProductAPI.Repository
{
    public interface IProductService
    {
        public Product AddProduct(Product product);
        public Product ModifyProduct(int id, Product product);
        public string DeleteProduct(int id);
        public List<Product> GetProducts();
        public Product GetbyId(int id);
        public Product GetbyName(string name);
    }
}
