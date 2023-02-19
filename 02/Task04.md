## Task 4

### Unit tests in ASP.NET Core MVC

1. Create a unit test project for `WebApp` and try to write 4 unit tests for different methods in controllers. You can use different framework for test (Nunit, xUnit)
and different library to mock (like Moq or NSubstitute).

Below you can find an example of the unit test for method in controller with using NUnit and Moq.

```cs
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
```
                  
