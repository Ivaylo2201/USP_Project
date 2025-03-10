﻿using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Api.Services;
using Api.Data_Transfer_Objects;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(DatabaseContext context, AuthService authService) : ControllerBase
    {
        private readonly DatabaseContext _context = context;
        private readonly AuthService _authService = authService;

        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserRequest request)
        {
            Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

            if (!EmailRegex.IsMatch(request.Email) || _context.Users.Any(u => u.Email == request.Email))
                return BadRequest(new { message = "Invalid email." });

            if (_context.Users.Any(u => u.Username == request.Username))
                return BadRequest(new { message = "Username already taken." });

            if (request.Password != request.PasswordConfirmation)
                return BadRequest(new { message = "Passwords do not match" });

            var user = new User { 
                Email = request.Email, 
                Username = request.Username, 
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Cart = new Cart()
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(Register), 
                new { id = user.Id }, 
                new { token = _authService.GenerateToken(user) 
            });
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginUserRequest request)
        {
            var user = _context.Users.Where(u => u.Email == request.Email).FirstOrDefault(); ;

            if (user is null)
                return NotFound(new { message = "User not found." });

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return BadRequest(new { message = "Invalid password." });

            return Ok(new { token = _authService.GenerateToken(user) });
        }
    }
}
