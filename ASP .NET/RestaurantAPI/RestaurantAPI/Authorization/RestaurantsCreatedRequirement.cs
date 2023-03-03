using Microsoft.AspNetCore.Authorization;
using System;

namespace RestaurantAPI.Authorization
{

    public class RestaurantsCreatedRequirement : IAuthorizationRequirement
    {
        public int RestaurantsCreated { get; set; }

        public RestaurantsCreatedRequirement(int restaurantsCreated)
        {
            RestaurantsCreated = restaurantsCreated;
        }
    }
}