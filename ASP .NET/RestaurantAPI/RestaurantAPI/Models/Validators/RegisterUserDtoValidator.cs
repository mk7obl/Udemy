using FluentValidation;
using RestaurantAPI.Entities;
using System;
using System.Linq;

namespace RestaurantAPI.Models.Validators
{


    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .MinimumLength(6);

            RuleFor(u => u.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });

        }
    }

}