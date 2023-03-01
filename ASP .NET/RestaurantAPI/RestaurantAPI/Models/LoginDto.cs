using System;

namespace RestaurantAPI.Models
{


    public class LoginDto
    {

        //bez walidacji dla uproszczenia

        public string Email { get; set; }
        public string Password { get; set; }
    }
}