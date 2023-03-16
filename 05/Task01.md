## Task 1

###  Update pagination in the search view

1. Add viewmodel which will be responsible for keeping collection of products and total number of pages.

```cs
 public class CountingProductViewModel
    {
        public IList<ProductViewModel> ProductViewModels { get; set; } = new List<ProductViewModel>();

        public int TotalCount { get; set; }
    }
```

2. Create a new partial view which will be using another partial displaying searched products (or other model you have) and it will contain navigation buttons. 

```html
<div id="navigation_buttons_container">
    <button id="btn_previous" class="btn btn-primary">Previous</button>
    @for (int i = 0; i < Model.TotalPages; i++)
    {
        <button id='@string.Format("btn{0}",i+1)' class="btn btn-primary btn_iteration" data-index="@i">@(i+1)</button>
    }
    <button id="btn_next" class="btn btn-secondary">Next</button>
</div>
```

3. Update jQuery calls from your parent view.

```js

        function getPagedData(){
            var url = '@Url.Action("Search","Products")'; 
            $.ajax({
                url: url,
                type: "POST",
                data: { "SearchString": $("#SearchString").val(), "PageNumber": index },
                success: onSuccess,
                failure: onFailure,
            })
        }

        $(document).on('click', "#btn_previous", () => {
            if (index > 0) {
                index--;
                getPagedData();
            }
        })

        $(document).on('click', "#btn_next", () => {
            index++;
            getPagedData();
        })

        $(document).on('click', ".btn_iteration", (e) => {
            index = $(e.currentTarget).data('index')
            getPagedData();
        })
```

4. Resolve all compile problems, inspect how it works. Think how you can improve and validate a proper behavior e.g. keeping maximum current page.
