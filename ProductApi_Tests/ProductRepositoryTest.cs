using ProductAPI.Model;
using ProductAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi_Tests
{
    public class ProductRepositoryTest
    {
        [Fact]
        public void AddProductSuccess()
        {
            Product product = new Product
            {
                productId = 1,
                productName = "Mobile",
                productBrand = "Apple",
                productPrice = 50000.0
            };
            var _productService = new ProductService();
            var addedProduct = _productService.AddProduct(product);
            Assert.NotNull(addedProduct);
            Assert.Equal(product, addedProduct);
        }
        [Fact]
        public void GetByIdSuccess()
        {
            int id = 1;
            var _productService = new ProductService();
            var addedProduct = _productService.GetbyId(id);
            Assert.Equal(addedProduct.productId, id);
        }
        [Fact]
        public void ModifyProductSuccess()
        {
            var _productService = new ProductService();
            int validProductId = 1;
            Product product = new Product
            {
                productName = "Mobile",
                productBrand = "HP",
                productPrice = 70000.0
            };
           
            var addedProduct = _productService.ModifyProduct(validProductId,product);

            Assert.Equal(validProductId, addedProduct.productId);
            Assert.Equal(product.productName, addedProduct.productName);
            Assert.Equal(product.productBrand, addedProduct.productBrand);
            Assert.Equal(product.productPrice, addedProduct.productPrice);

        }
        [Fact]
        public void DeleteProductSuccess()
        {
            int id = 1;
            var _productService = new ProductService();
            var res = _productService.DeleteProduct(id);
            Assert.Equal("Mobile",res);
        }
    }
}
