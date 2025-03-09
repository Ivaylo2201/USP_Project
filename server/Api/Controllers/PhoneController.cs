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

                return CreatedAtAction(nameof(CreatePhone), new { id = phone.Id }, phone);
            }
            catch (DbUpdateException e)
            {
                return BadRequest(new { message = "There was an error with the database operation.", details = e.InnerException?.Message });
            }
        }

        [HttpGet]
        public IActionResult GetPhones()
        {
            var phones = _context.Phones.ToList();
            return Ok(phones);
        }
    }
}
