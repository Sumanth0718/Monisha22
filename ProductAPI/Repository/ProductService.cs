using ProductAPI.Model;
using System.Web.Mvc;

namespace ProductAPI.Repository
{
    public class ProductService : IProductService
    {
        public List<Product> products;
        public ProductService()
        {
            products = new List<Product>()
            {
                new Product{productId=1, productName="Mobile", productBrand="Apple", productPrice=50000.0},
                new Product{productId=2, productName="Laptop", productBrand="Lenovo", productPrice=150000.0}
            };      
        }
        // Method for adding the product into the product list.
        public Product AddProduct(Product product)
        {
            products.Add(product);
            return product;
        }
        // Method for updating the details of a specific product.
        public Product ModifyProduct(int id, Product product)
        {
            var product1 = products.FirstOrDefault(p => p.productId == id);
            if(product1 != null)
            {
                product1.productName = product.productName;
                product1.productBrand = product.productBrand;
                product1.productPrice = product.productPrice;
                return product1;
            }
            return null;
        }
        //Method to delete a product from the product list.
        public string DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(x => x.productId == id);
            if(product != null)
            {
                products.Remove(product);
                return product.productName;
            }
            return null;
        }
        //Method to retrieve details all the products.
        public List<Product> GetProducts()
        {
            return products;
        }
        //Method to retrieve a particular product by it's ID.
        public Product GetbyId(int id)
        {
            var product = products.FirstOrDefault(x => x.productId == id);
            if (product != null)
            {
                return product;
            }
            return null;
        }
        //Method to retrieve a particular product by it's Name.
        public Product GetbyName(string name)
        {
            var product = products.FirstOrDefault(x => x.productName == name);
            if(product != null)
            {
                return product;
            }
            return null;
        }
    }
}
