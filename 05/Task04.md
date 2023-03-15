## Task 4

###  Create a CRUD operation in gRPC

1. In csproj file add `Grpc.Tools` package (you can also install it via `Nuget Package Manager`).

```xml
<PackageReference Include="Grpc.Tools" Version="2.40.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
</PackageReference>
```

2. Create a proto file used as your endpoints.

```proto
service ProductsGrpc {
    rpc GetAll(Empty) returns(Products);
    rpc GetById(IdFilter) returns(Product);
    rpc Post(Product) returns(Product);
    rpc Put(Product) returns(Product);
    rpc Delete(IdFilter) returns(Empty);
}
message Empty {}
message Product {
    int32 Id = 1;
    string Name = 2;
    string Description = 3;
    double Price = 4;
    int32 ProductTypeId = 5;
}
message IdFilter {
    int32 Id = 1;
}
message Products {
    repeated Product items = 1;
}
```

3. Create a service for handle all contracts for all CRUD operations.

```cs
 public class ProductGrpcService : ProductsGrpc.ProductsGrpcBase
    {
        private readonly OponeoCustomerManagementMVCWebAppContext _dbContext;
        public ProductGrpcService(OponeoCustomerManagementMVCWebAppContext oponeoCustomerManagementMVCWebAppContext)
        {
            _dbContext = oponeoCustomerManagementMVCWebAppContext;
        }

        public override Task<Products> GetAll(Empty request, ServerCallContext context)
        {
            Products products = new ();
            products.Items.AddRange(_dbContext.Product.Select(x => new Product
            {
                Description = x.Description,
                Name = x.Name,
                ProductTypeId = x.ProductTypeId,
                Price = x.Price,
                Id = x.Id
            }));

            return Task.FromResult(products);
        }
```
