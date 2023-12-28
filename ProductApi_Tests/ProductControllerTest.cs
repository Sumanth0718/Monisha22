using ikvm.runtime;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NPOI.SS.Formula.Functions;
using ProductAPI.Controllers;
using ProductAPI.Model;
using ProductAPI.Repository;
using System.Net;
using System.Net.Http.Formatting;

namespace ProductApi_Tests
{
    public class ProductControllerTest
    {
        [Fact]
        public void AddProductSuccess()
        {
            var mockRepo = new Mock<IProductService>();
            var _productController = new ProductController(mockRepo.Object);
            var product = new Product 
            { 
                productId = 1, 
                productName = "Tab",
                productBrand= "Asus",
                productPrice = 20000
            };
            var addedProduct = _productController.AddProduct(product);
            Assert.Equal(addedProduct as IEnumerable<T>, product as IEnumerable<T>);
        }
        [Fact]
        public void GetByIdSuccess()
        {
            var mockRepo = new Mock<IProductService>();
            var _productController = new ProductController(mockRepo.Object);
            int id = 1;
            mockRepo.Setup(repo => repo.GetbyId(id))
                          .Returns(new Product { productId = id, productName = "Tab" });
            var product = _productController.GetbyId(id);
            var okResult = Assert.IsType<OkObjectResult>(product);
            var gotproduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(id, gotproduct.productId);
        }
        [Fact]
        public void ModifyProductSuccess()
        {
            // Arrange
            var mockRepository = new Mock<IProductService>();
            var controller = new ProductController(mockRepository.Object);
            int validProductId = 1;
            var updatedProduct = new Product { productId = validProductId, productName = "Updated Product" };

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetbyId(validProductId))
                          .Returns(new Product { productId = validProductId, productName = "Original Product" });

            // Act
            var result = controller.ModifyProduct(validProductId, updatedProduct);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.NotNull(product);
            Assert.Equal(updatedProduct.productId, product.productId);
            Assert.Equal(updatedProduct.productName, product.productName);
        }
        [Fact]
        public void DeleteProductSuccess()
        {
            var mockRepo = new Mock<IProductService>();
            var _productController = new ProductController(mockRepo.Object);
            int id = 1;
            
            mockRepo.Setup(repo => repo.GetbyId(id))
                          .Returns(new Product { productId = id, productName = "Testing" });

            var res = _productController.DeleteProduct(id);
            
            var okResult = Assert.IsType<OkObjectResult>(res);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Contains(id.ToString(), message);
        }
        [Fact]
        public void AddProductFailure()
        {
            var mockRepository = new Mock<IProductService>();
            var _productController = new ProductController(mockRepository.Object);
            Product invalidProduct = null;
            var result = _productController.AddProduct(invalidProduct);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void GetByIdFailure()
        {
            var mockRepository = new Mock<IProductService>();
            var controller = new ProductController(mockRepository.Object);
            int invalidProductId = 999;
            mockRepository.Setup(repo => repo.GetbyId(invalidProductId)).Returns((Product)null);
            var result = controller.GetbyId(invalidProductId);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void ModifyProductFailure()
        {
            var mockRepository = new Mock<IProductService>();
            var controller = new ProductController(mockRepository.Object);
            int invalidProductId = 999;
            var updatedProduct = new Product { productId = invalidProductId, productName = "Updated Product" };

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetbyId(invalidProductId))
                          .Returns((Product)null);

            // Act
            var result = controller.ModifyProduct(invalidProductId,updatedProduct);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public void DeleteProductFailure()
        {
            var mockRepository = new Mock<IProductService>();
            var controller = new ProductController(mockRepository.Object);
            int invalidProductId = 999;
            mockRepository.Setup(repo => repo.GetbyId(invalidProductId)).Returns((Product)null);
            var result = controller.DeleteProduct(invalidProductId);
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}