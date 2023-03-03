using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{

    public class RestaurantsCreatedRequirementHandler : AuthorizationHandler<RestaurantsCreatedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RestaurantsCreatedRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}