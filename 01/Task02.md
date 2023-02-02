## Task 2

### Using built-in IoC in ASP.NET Core MVC

1. Use three the same interfaces to create different registration (interfaces for Scoped, Transient and Singleton). With a guid as a property.

```cs
 public interface ISingletonService
    {
        Guid UniqueGuid { get; }
    }
```

2. Register all three interfaces into one instance which return guid all the time. Remember to create guid in the constructor of that class not in getter.

3. Create a registration for IoC. Similar like below.

```cs

builder.Services.AddScoped<IScopedService, SomeService>();
builder.Services.AddSingleton<ISingletonService, SomeService>();
builder.Services.AddTransient<ITransientService, SomeService>();
```

4. Create controller and inject two instances of each interface.

```cs
  private readonly IScopedService _scopedService1;
        private readonly IScopedService _scopedService2;
        private readonly ITransientService _transientService1;
        private readonly ITransientService _transientService2;
        private readonly ISingletonService _singletonService1;
        private readonly ISingletonService _singletonService2;

        public HomeController(ILogger<HomeController> logger,IScopedService scopedService1,
            IScopedService scopedService2, ITransientService transientService1,
            ITransientService transientService2, ISingletonService singletonService1, 
            ISingletonService singletonService2)
        {
            _logger = logger;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            _transientService1 = transientService1;
            _transientService2 = transientService2;
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
        }
```

5. Add action method to return values like that (create a proper model class).

```cs
  public IActionResult ShowServices()
        {
            return View(new IoCExample
            {
                Scoped1 = _scopedService1.GetGuid,
                Scoped2 = _scopedService2.GetGuid,
                Singleton1 = _singletonService1.GetGuid,
                Singleton2 = _singletonService2.GetGuid,
                Transient1 = _transientService1.GetGuid,
                Transient2 = _transientService2.GetGuid
            });
        }
```

6. Create a view to inspect results

```html
@model WebApplication2.Models.IoCExample
@{
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";
}


<h1>Szczegóły wywołań</h1>

<div>
    <h4>Wywołanie serwisów z 1</h4>
    <hr />
    <ul>
        <li>@string.Format("Scoped1: {0}",Model.Scoped1)</li>
        <li>@string.Format("Transient1: {0}",Model.Transient1)</li>
        <li>@string.Format("Singleton1: {0}",Model.Singleton1)</li>
    </ul>
</div>

<div>
    <h4>Wywołanie serwisów z 2</h4>
    <hr />
    <ul>
        <li>@string.Format("Scoped2: {0}",Model.Scoped2)</li>
        <li>@string.Format("Transient2: {0}",Model.Transient2)</li>
        <li>@string.Format("Singleton2: {0}",Model.Singleton2)</li>
    </ul>
</div>
```
