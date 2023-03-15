## Task 3

###  Create a gRPC ASP.NET Core project

1. In Visual Studio create a gRPC project by choosing `ASP.NET Core gRPC Service`. Investigate if the project compiles.

2. Verify the structure of proto files and service.

3. Include package reference in csproj file to use reflection.

```xml
    <PackageReference Include="Grpc.AspNetCore.Server.Reflection" Version="2.51.0" />
```

4. Add those lines in `Program.cs` to use reflection in gRPC.

```cs
  builder.Services.AddGrpcReflection();
```

```cs

IWebHostEnvironment env = app.Environment;

if (env.IsDevelopment())
{
    app.MapGrpcReflectionService();
}
```
5. Verify in Postman if you can connect to gRPC service and call the endpoint
