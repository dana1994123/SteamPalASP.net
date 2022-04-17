﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{steamid/games}")]
        public async Task<IActionResult> GetGames(string steamid)
        {
            try
            {
                var person = await _logger.Persons.FindAsync(new Guid(id));
                var personDto = new 
                {
                    Id = person.Id,
                    FullName = $"{person.FirstName} {person.LastName}"
                };
                var response = new Response<PersonDTO>(personDto);


                return Ok(response);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

