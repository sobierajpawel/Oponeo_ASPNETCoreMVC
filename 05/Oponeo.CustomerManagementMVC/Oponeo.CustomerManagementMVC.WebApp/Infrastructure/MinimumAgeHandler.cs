using Microsoft.AspNetCore.Authorization;

namespace Oponeo.CustomerManagementMVC.WebApp.Infrastructure
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var claim = context.User.FindFirst("job_start_date");
            if (claim == null)
                return Task.CompletedTask;

            if (!DateTime.TryParse(claim.Value, out DateTime startJobDate))
                return Task.CompletedTask;

            int experienceYears = ((int)(DateTime.Now - startJobDate).TotalDays / 365);
            if (experienceYears >= requirement.MinimumAge)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
