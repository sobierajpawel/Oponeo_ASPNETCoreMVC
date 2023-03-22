## Task 3

###  Create a servcer streaming gRPC.

1. In csproj file add `Grpc.Tools` package (you can also install it via `Nuget Package Manager`) if you do not have this package.

```xml
<PackageReference Include="Grpc.Tools" Version="2.40.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
</PackageReference>
```

2. Create a proto file used as your endpoints.

```proto
syntax = "proto3";

option csharp_namespace = "Oponeo.CustomerManagement.GrpcService.Protos";

service ProductGrpcStreamRequestResponse{
	rpc GetAll(EmptyRequest) returns(stream ProductResponseStream);
}

message EmptyRequest{}

message ProductResponseStream{
	int32 Id = 1;
	string Name = 2;
	string Description = 3;
	double Price = 4;
	int32 ProductTypeId = 5;
}

```

3. Create a service for handle contract. Pretend the behavior of the stream by using Task.Delay.

```cs
 public class ProductGrpStream : ProductGrpcStreamRequestResponse.ProductGrpcStreamRequestResponseBase
    {
        private readonly ProductService _productService;

        public ProductGrpStream(ProductService productService)
        {
            _productService = productService;
        }

        public override async Task GetAll(Protos.EmptyRequest request, IServerStreamWriter<ProductResponseStream> responseStream, ServerCallContext context)
        {
            var products = _productService.Get().ToList();
            var i = 0;

            while (!context.CancellationToken.IsCancellationRequested && i < products.Count())
            {
                await Task.Delay(500); 

                var productResponseStream = new ProductResponseStream
                {
                    Id = products[i].Id,
                    Description = products[i].Description,
                    Name = products[i].Name,
                    Price = products[i].Price,
                    ProductTypeId = products[i].ProductTypeId
                };

                i++;
                await responseStream.WriteAsync(productResponseStream);
            }
        }
    }
```

4. Compile it and test it via Postman or other tool.
