using Grpc.Core;
using Oponeo.CustomerManagement.GrpcService;
using Oponeo.CustomerManagementMVC.Services.Products;

namespace Oponeo.CustomerManagement.GrpcService.Services
{
    public class ProductGrpcService : ProductGrpcRequestResponse.ProductGrpcRequestResponseBase
    {
        private readonly ProductService _productService;

        public ProductGrpcService(ProductService productService)
        {
            _productService = productService;
        }

        public override Task<ProductResponse> Add(ProductRequest request, ServerCallContext context)
        {
            var product = new CustomerManagementMVC.Domain.Models.Product
            {
                Description = request.Description,
                Name = request.Name,
                Price = request.Price,
                ProductTypeId = request.ProductTypeId
            };

            _productService.AddOrUpdate(product);

            return Task.FromResult(new ProductResponse
            {
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductTypeId = product.ProductTypeId
            });
        }

        public override Task<ProductsResponse> GetAll(EmptyRequest request, ServerCallContext context)
        {
            var products = _productService.Get();
            ProductsResponse productsResponse = new();
            productsResponse.Items.AddRange(products.Select(x => new ProductResponse
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                Price = x.Price,
                ProductTypeId = x.ProductTypeId
            }));

            return Task.FromResult(productsResponse);
        }

        public override Task<ProductResponse> GetById(IdRequest request, ServerCallContext context)
        {
            var product = _productService.GetById(request.Id);

            return Task.FromResult(new ProductResponse
            {
                Id = product.Id,
                Description = product.Description,
                ProductTypeId = product.ProductTypeId,
                Name = product.Name,
                Price = product.Price
            });
        }

        public override Task<EmptyResponse> Remove(IdRequest request, ServerCallContext context)
        {
            _productService.Remove(request.Id);
            return Task.FromResult(new EmptyResponse());
        }

        public override Task<ProductResponse> Update(ProductRequest request, ServerCallContext context)
        {
            var product = new CustomerManagementMVC.Domain.Models.Product
            {
                Id = request.Id,
                Description = request.Description,
                Name = request.Name,
                Price = request.Price,
                ProductTypeId = request.ProductTypeId
            };

            _productService.AddOrUpdate(product);

            return Task.FromResult(new ProductResponse
            {
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductTypeId = product.ProductTypeId
            });
        }
    }
}
