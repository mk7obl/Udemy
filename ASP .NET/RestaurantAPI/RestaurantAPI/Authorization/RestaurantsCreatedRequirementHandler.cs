using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{

    public class RestaurantsCreatedRequirementHandler : AuthorizationHandler<RestaurantsCreatedRequirement>
    {
        private readonly RestaurantDbContext _context;

        public RestaurantsCreatedRequirementHandler(RestaurantDbContext context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RestaurantsCreatedRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdRestaurantsCount = _context
                .Restaurants
                .Count(r => r.CreatedById == userId);

            if (createdRestaurantsCount >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}