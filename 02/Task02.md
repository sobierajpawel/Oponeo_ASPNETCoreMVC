## Task 2

### Using IoC in the view and use validation

1. Create a view model class in the `Web` project which will be mapped with a domain class and display on view.

2. Change in all controllers and views a base class to this viewmodel. For the time being, create a mapper class to mapp viewmodel with domain class or use `AutoMapper`
or write the code directly in the controllers.

3. Add validation rules to this viewmodel like in below example. Inspect how it works

```cs
 public class ProductViewModel
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength=5)]
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Za-z]+$")]
        public string Description { get; set; }

        [DisplayName("Product Price")]
        [Range(1,1000)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double Price { get; set; }
    }
```

4. Create a service in the `Application` project which will give you some statistics e.g.:
- average price of product/average of age of customers (depending on domain you have)
- count of records

5. Register new service as a scoped and inject it in view

```html
  @inject Oponeo.CustomerManagementMVC.Services.Statistics.ProductStatisticService productStatisticsService
```

6. Use methods provides by registered service directly on the particular view.
