## Task 4

###  Create CQRS architecture with MediatR

1. Create Order and OrderLine domain objects in the `Domain` project. Add migration and update database.

2. Install `MediatR` package from nuget.

3. Register mediatR in MVC project like this.

```cs
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddOrdersCommandHandler).Assembly));
```

4. Create projects for `Queries` and `Commands`

5. Add a command class and handler for it. 

```cs
public class AddOrdersCommand : IRequest<bool>
{
}
```

```cs
 public class AddOrdersCommandHandler : IRequestHandler<AddOrdersCommand, bool>
    {
        public AddOrdersCommandHandler()
        {
        }

        public async Task<bool> Handle(AddOrdersCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }
    }
```

6. Implement logic for saving in the database an order.

7. In the controller for orders inject IMediatr and add proper method with sending a command.

```cs
 private readonly ProductService _productService;
        private readonly IMediator _mediator;
        public OrderController(ProductService productService, IMediator mediator)
        {
            _productService = productService;
            _mediator = mediator;
        }
```

```cs
 [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel orderViewModel)
        {
            await _mediator.Send(new AddOrdersCommand());
            return RedirectToAction(nameof(Index));
        }
 ```
 
 8. Do the same for query operation to get order data and display them in MVC project.
