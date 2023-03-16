## Task 2

###  Use Json as a result from controller

1. Create a service method on separate class which will be used as a statistic source. You can create a group of values which presents product type name with the
corresponding items included in the type.

```cs
public IEnumerable<(string, int)> GetTotalProductTypes()
{
    var products =  this._productRepository.GetAll();

    return products.GroupBy(x => x.ProductType.TypeName)
        .Select(c => (c.Key, c.Count()))
        .ToList();
}
```

2. Create a controller with an action result method which returns Json as a result.

```cs
 return Json(_productStatisticsService.GetTotalProductTypes().Select(x=> new
            {
                key = x.Item1,
                value = x.Item2
            }));
```

3. Add graph library to the project.

```html
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.js"></script>
```

4. Write an AJAX script which calls the endpoint and draw a chart.

```js
@section Scripts {
    <script type="text/javascript">
        $(document).ready(function () {
            var url = '@Url.Action("GetStats","Home")';
            $.ajax({
                url: url,
                type: "GET",
                success: drawGraph,
            });
        });

        function drawGraph(values) {
            console.log(values);

            var barColors = ["red", "green", "blue", "orange", "brown"];

            new Chart("myChart", {
                type: "bar",
                data: {
                    labels: values.map(a => a.key),
                    datasets: [{
                        backgroundColor: barColors,
                        data: values.map(a => a.value),
                    }]
                },
            });
        }
    </script>
}
```

5. Try to beautify the graph by adding legend, adjust scale etc.
