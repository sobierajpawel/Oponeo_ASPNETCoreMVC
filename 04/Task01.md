## Task 1

###  Create next/previous behavior for search table

1. Add html code to use next/previous button in the search view.

```html
<div id="navigationButtons" style="display:none">
    <button id="btnPrevious" type="button" class="btn btn-secondary">Previous</button>
    <button id="btnNext" type="button" class="btn btn-primary">Next</button>
</div>
```

2. Write a javascript script with using jQuery and AJAX to getData() after clicking with sending additional pageNumber.

```js
        let index = 0;
        $(document).ready(()=>{
            $("#btnNext").on("click", function () {
                index++;
                getData();
            });

            $("#btnPrevious").on("click", function () {
                index--;
                getData();
            });
        });

        function getData(){
            onBegin();
            var url = '@Url.Action("Search", "Products")'
            $.ajax({
                type: "POST",
                url: url,
                data: { "SearchString": $("#SearchString").val(), "PageNumber": index },
                success: onSuccess,
                failure: onFailure,
                error : function(data){
                    console.log(data);
                }
            })
        }
```

3. Prepare your controller and service to implement skipping and taking mechanism. You can use linq to do that.

```cs
productsViewModels = productsViewModels.Skip(pageNumber * pageCount).Take(pageCount).ToList();
```

4. Make your application more useful - disable button prev/next when there is no records in the prev/next page. Think how to use `IMemoryCache` to store data.
