using Microsoft.AspNetCore.Authorization;
using System;

namespace RestaurantAPI.Authorization
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