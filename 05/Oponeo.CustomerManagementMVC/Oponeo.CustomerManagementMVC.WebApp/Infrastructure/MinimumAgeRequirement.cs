using Microsoft.AspNetCore.Authorization;

namespace Oponeo.CustomerManagementMVC.WebApp.Infrastructure
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; set; } 
        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;    
        }
    }
}
