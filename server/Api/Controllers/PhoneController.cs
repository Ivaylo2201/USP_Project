using Api.Data_Transfer_Objects;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneController(DatabaseContext context) : ControllerBase
    {
        private readonly DatabaseContext _context = context;

        [HttpPost]
        public IActionResult CreatePhone([FromBody] CreatePhoneBody request)
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
        public IActionResult GetPhones([FromQuery] string? brand, string? model, string? color, decimal? max, string? sort = "asc")
        {
            // TODO: SORT by field and direction

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

            var phones = query.Select(p => new
            {
                p.Id,
                Brand = p.Brand.Name,
                Model = p.Model.Name,
                Color = p.Color.Name,
                p.Price,
                //Image = p.ImageUrl
            })
            .ToList();

            return Ok(phones);
        }
    }
}
