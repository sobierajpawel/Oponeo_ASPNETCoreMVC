using Microsoft.AspNetCore.Mvc.Filters;

namespace Oponeo.CustomerManagementMVC.WebApp.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LoggingFilter : Attribute, IActionFilter
    {
        private readonly ILogger _logger;
        public LoggingFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("ActionFilter logger");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action executed {context.ActionDescriptor.DisplayName} at time {DateTime.Now}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Action executing {context.ActionDescriptor.DisplayName} at time {DateTime.Now}");
        }
    }
}
