## Task 2

###  Create a complex form with collection

1. Create a viewmodels for Orders and orderlines (do not create domain model classes yet - it will be a part of CQRS exercise).

```cs
public class OrderViewModel
{
        public string OrderNumber { get; set; }

        public List<OrderLineViewModel> Lines { get; set; }
}
```
```cs
 public class OrderLineViewModel
{
        [DisplayName("Count")]
        public int Count { get; set; }
        [DisplayName("Product")]
        public int SelectedProductId { get; set; }
}
```

2. Create a view to POST data.


```html
@model Oponeo.CustomerManagementMVC.WebApp.Models.OrderViewModel

@{
    ViewData["Title"] = "Create";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h1>Create</h1>

<h4>Product</h4>
<hr />
<div class="row">
    <div class="col-md-4">
        <form asp-action="Create">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="OrderNumber" class="control-label"></label>
                <input asp-for="OrderNumber" class="form-control" />
                <span asp-validation-for="OrderNumber" class="text-danger"></span>
            </div>
            <div class="form-group">
                <table class="table">
                    <thead>
                        <tr>
                            <th>
                                @Html.DisplayNameFor(model => model.Lines[0].SelectedProductId)
                            </th>
                            <th>
                                @Html.DisplayNameFor(model => model.Lines[0].Count)
                            </th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        @for(int i=0;i<Model.Lines.Count;i++)
                        {
                            <tr>
                                <td>
                                    <label asp-for="Lines[i].SelectedProductId" class="control-label"></label>
                                    <select asp-items="ViewBag.Products" asp-for="Lines[i].SelectedProductId" class="form-control"></select>
                                </td>
                                <td>
                                    <label asp-for="Lines[i].Count" class="control-label"></label>
                                    <input asp-for="Lines[i].Count" class="form-control" />
                                    <span asp-validation-for="Lines[i].Count" class="text-danger"/>
                                </td> 
                            </tr>
                        }
                    </tbody>
                </table>
            </div>
           
            <div class="form-group">
                <input type="submit" value="Create" class="btn btn-primary" />   
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action="Index">Back to List</a>
</div>
```

3. Add controller methods to handle collection like this (do not create saving to database in post only check if collection of elements has been addded).


```cs
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Products = _productService.Get()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()));

            var orderViewModel = new OrderViewModel();
            orderViewModel.Lines = new List<OrderLineViewModel>();
            orderViewModel.Lines.Add(new OrderLineViewModel());
            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult Create(OrderViewModel orderViewModel)
        {
            ///
        }
```

4. Add a html code for an extra button to addline. Use `formaction` attribute to differentiate actions in the same form.

```html
            <div class="form-group">
                <input type="submit" value="Create" formaction="/Order/Create" class="btn btn-primary" />
                <input type="submit" value="AddNewLine" formaction="/Order/AddLine" class="btn btn-secondar" />
            </div>
```

5. Add a controller methhod do add a line for order.

```cs
        [HttpPost]
        public IActionResult AddLine(OrderViewModel orderViewModel)
        {
            orderViewModel.Lines.Add(new OrderLineViewModel());
            ViewBag.Products = _productService.Get()
               .Select(x => new SelectListItem(x.Name, x.Id.ToString()));
            return View("Create",orderViewModel);
        }
```

6. Inspect if the mechanism works fine, add index view with all orders.
