using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class ReastaurantController : ControllerBase
    {
        private readonly DbContext _dbcontext;
        private readonly IAccountService _accountService;

        public ReastaurantController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }
    }
}