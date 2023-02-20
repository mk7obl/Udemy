using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI
{

    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }

            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "Kentucky Fried Chicken, najlepszy sosik pozdro",
                    ContactEmail = "contact@kfc.pl",
                    HasDelivery = true,

                    Dishes = new List<Dish>
                    {
                        new Dish()
                        {
                            Name = "B-smart",
                            Price = 4.90M,
                        },

                        new Dish()
                        {
                            Name = "Chicken nuggets",
                            Price = 7.99M,
                        },
                    },

                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Opolska",
                        PostalCode = "30-290"
                    }
                },

                new Restaurant()
                {
                    Name = "McDonald's",
                    Category = "Fast Food",
                    Description = "mamy calkiem dobre frytki",
                    ContactEmail = "contact@mak.pl",
                    HasDelivery = true,
                    Dishes = new List<Dish>
                    {
                        new Dish()
                        {
                            Name = "Frytki",
                            Price = 2M,
                        },

                        new Dish()
                        {
                            Name = "Chicken nuggets",
                            Price = 4.99M
                        },
                    },

                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Pawia",
                        PostalCode = "30-290"
                    }
                },
            };
            return restaurants;
        }
    }
}