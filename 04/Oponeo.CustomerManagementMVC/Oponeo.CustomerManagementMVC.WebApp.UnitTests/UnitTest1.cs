using Microsoft.AspNetCore.Mvc;
using Moq;
using Oponeo.CustomerManagementMVC.Domain.Models;
using Oponeo.CustomerManagementMVC.Services.Products;
using Oponeo.CustomerManagementMVC.WebApp.Controllers;
using Oponeo.CustomerManagementMVC.WebApp.Models;
using System.Collections.Generic;

namespace Oponeo.CustomerManagementMVC.WebApp.UnitTests
{
    public class ProductControllerTest
    {
        [Test]
        public async Task Index_Should_Return_Collection_Of_Products_If_Successful()
        {
            //Arrange
            Mock<ProductService> mockedProductService = new Mock<ProductService>();
            mockedProductService.Setup(x => x.Get()).Returns(new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test"
                },
                new Product
                {
                    Id = 2,
                    Name = "Test",
                    Description = "Test"
                },
            });
            ProductsController productController = new ProductsController(mockedProductService.Object);

            //Act
            var result = await productController.Index();

            //Arrange
            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResultModel = ((ViewResult)result).Model;
            Assert.IsAssignableFrom<List<ProductViewModel>>(viewResultModel);
            var productViewModel = (IEnumerable<ProductViewModel>)viewResultModel;
            Assert.AreEqual(2, productViewModel.Count());
        }
    }
}