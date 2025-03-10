using Api.Data_Transfer_Objects;
using Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneController(DatabaseContext context) : ControllerBase
    {
        private readonly DatabaseContext _context = context;

        [HttpPost]
        public IActionResult CreatePhone([FromBody] CreatePhoneRequest request)
        {
            try
            {
                var phone = new Phone
                {
                    BrandId = request.BrandId,
                    ModelId = request.ModelId,
                    ColorId = request.ColorId,
                    Price = request.Price
                };

                _context.Phones.Add(phone);
                _context.SaveChanges();

                return CreatedAtAction(nameof(CreatePhone), new { message = "Phone created successfully." });
            }
            catch (DbUpdateException e)
            {
                //return BadRequest(new { message = "An error has occurred." });
                return BadRequest(new { message = e.InnerException?.Message });
            }
        }

        [HttpGet]
        [Route("public")]
        public IActionResult GetPublicPhones([FromQuery] string? brand, string? model, string? color, decimal? max, string? sort = "asc")
        {
            var query = this.GetPhones(brand, model, color, max, sort);
            var phones = query.Select(p => new
            {
                p.Id,
                Brand = p.Brand.Name,
                Model = p.Model.Name,
                Color = p.Color.Name,
                p.Price,
                //Image = p.ImageUrl
                isLiked = false
            })
            .ToList();

            return Ok(phones);
        }

        [HttpGet]
        [Authorize]
        [Route("authenticated")]
        public IActionResult GetAuthenticatedPhones([FromQuery] string? brand, string? model, string? color, decimal? max, string? sort = "asc")
        {
            if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id))
            {
                var user = _context.Users.Include(u => u.Cart).Where(u => u.Id == id).FirstOrDefault();

                if (user == null)
                    return Unauthorized(new { message = "The provided token was invalid." });

                var query = this.GetPhones(brand, model, color, max, sort);
                var phones = query.Select(p => new
                {
                    p.Id,
                    Brand = p.Brand.Name,
                    Model = p.Model.Name,
                    Color = p.Color.Name,
                    p.Price,
                    //Image = p.ImageUrl
                    isLiked = p.LikedBy.Contains(user)
                })
                .ToList();

                return Ok(phones);
            }
            else
            {
                return BadRequest(new { message = "Something went wrong." });
            }          
        }

        private IQueryable<Phone> GetPhones(string? brand, string? model, string? color, decimal? max, string? sort)
        {
            var query = _context.Phones
                    .Include(p => p.Brand)
                    .Include(p => p.Model)
                    .Include(p => p.Color)
                    .AsQueryable();

            if (!string.IsNullOrEmpty(brand))
                query = query.Where(p => p.Brand.Name.Contains(brand));

            if (!string.IsNullOrEmpty(model))
                query = query.Where(p => p.Model.Name.Contains(model));

            if (!string.IsNullOrEmpty(color))
                query = query.Where(p => p.Color.Name.Contains(color));

            if (max.HasValue)
                query = query.Where(p => p.Price <= max.Value);

            query = sort == "asc" ? query.OrderBy(p => ((double)p.Price)) :
                                    query.OrderByDescending(p => ((double)p.Price));

            return query;
        }

        [HttpPost]
        [Authorize]
        [Route("like")]
        public IActionResult LikePhone([FromBody] LikePhoneRequest request)
        {
            if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id))
            {
                var phone = _context.Phones.Find(request.PhoneId);
                var user = _context.Users.Find(id);

                if (phone != null && user != null)
                {
                    if (user.LikedPhones.Any(p => p.Id == phone.Id))
                    {
                        phone.LikedBy.Remove(user);
                    }
                    else
                    {
                        phone.LikedBy.Add(user);
                    }

                    _context.SaveChanges();
                    return Ok(new { message = "Phone liked successfully." });
                }

                return BadRequest(new { message = "Something went wrong." });
                
            }
            else
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }
    }
}
