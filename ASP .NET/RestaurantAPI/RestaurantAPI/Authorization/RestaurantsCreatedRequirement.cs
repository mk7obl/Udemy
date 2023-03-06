using Microsoft.AspNetCore.Authorization;
using System;

namespace RestaurantAPI.Authorization
{

    public class RestaurantsCreatedRequirement : IAuthorizationRequirement
    {
        public int MinimumRestaurantsCreated { get; set; }

        public RestaurantsCreatedRequirement(int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = minimumRestaurantsCreated;
        }
    }
}