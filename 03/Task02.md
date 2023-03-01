## Task 2

###  Using AJAX Form in ASP.NET Core MVC

1. Add jQuery and AJAX unbotrusive. You can use CDN or keep them locally. Add them to the `head` section in the application.

```html
<script src="https://code.jquery.com/jquery-3.6.3.min.js"
            integrity="sha256-pvPw+upLPUjgMXY0G+8O0xUf+/Im1MZjXxxgOcBQBXU="
            crossorigin="anonymous"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-ajax-unobtrusive/3.2.6/jquery.unobtrusive-ajax.js"></script>
```

2. Write a new view named `Search` and add a link in the layout view to navigate to that page.

3. Create a view with a corresponding viewmodeL allowing using search string. 

```cs
  public class SearchViewModel
    {
        public string SearchStr { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
```

```html
@model Oponeo.CustomerManagementMVC.WebApp.Models.SearchViewModel


<h1>Search</h1>

<form asp-action="Search" asp-controller="Products"
      data-ajax="true" data-ajax-method="POST"
      data-ajax-begin="OnBegin" data-ajax-failure="OnFailure"
      data-ajax-success="OnSuccess" data-ajax-complete="OnComplete">
    <div class="container">
        <table class="table table-condensed">
            <tr>
                <td>Search : </td>
                <td><input type="text" asp-for="SearchStr" class="form-control" /></td>
            </tr>
        </table>
    </div>
</form>

<div id="result" style="display:none">
    <partial name="~/Views/Products/GetProducts.cshtml" model="Model.Products" />
</div>

<div id="progress" style="display:none">
    <h4>Wyszukiwanie</h4>
</div>

@section Scripts {
<script type="text/javascript">
 function OnBegin() {
    $("#progress").show();
}
 
function OnFailure(response) {
    alert("Error occured.");
}
 
function OnSuccess(response) {
    $("#result").html(response);
    $("#result").show();
}
 
function OnComplete() {
    $("#progress").hide();
 }
</script>
}

```

4. Add searching logic in the controller and corresponding service class. You can use a new service class for that purpose.

```cs
public async Task<IActionResult> Search()
{
  var searchViewModel = new SearchViewModel();
  searchViewModel.Products = new List<ProductViewModel>();
  return View(searchViewModel);
}

[HttpPost]
public async Task<IActionResult> Search(SearchViewModel searchViewModel)
{
  var productsViewModels = new List<ProductViewModel>();
  await Task.Delay(2000);
  foreach (var product in _productService.Get())
  {
      productsViewModels.Add(new ProductViewModel
      {
        Description = product.Description,
        Name = product.Name,
        Price = product.Price,
        Id = product.Id
       });
   }
   searchViewModel.Products = productsViewModels
      .Where(x => x.Name == searchViewModel.SearchStr || x.Description == searchViewModel.SearchStr)
      .ToList();

   return PartialView("GetProducts", searchViewModel.Products);
}
```

5. Inspect how the whole process works.
