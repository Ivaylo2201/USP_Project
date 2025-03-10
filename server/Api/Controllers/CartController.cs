using Api.Data_Transfer_Objects;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController(DatabaseContext context) : ControllerBase
    {
        private readonly DatabaseContext _context = context;

        [HttpGet]
        [Authorize]
        public IActionResult GetCartItems()
        {
            if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id))
            {
                var cart = _context.Carts
                    .Where(c => c.Id == id)
                    .Include(c => c.Items)
                    .ThenInclude(i => i.Phone)
                    .Select(c => new
                    {
                        CartId = c.Id,
                        Items = c.Items.Select(i => new
                        {
                            i.Id,
                            i.Quantity,
                            Phone = new
                            {
                                brand = i.Phone.Brand.Name,
                                model = i.Phone.Model.Name,
                                color = i.Phone.Color.Name,
                                price = i.Phone.Price
                            },
                            price = i.Quantity * i.Phone.Price
                        })
                    })
                    .FirstOrDefault();

                return Ok(cart);                
            }
            else
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [HttpPost]
        [Authorize]
        [Route("add")]
        public IActionResult AddPhoneToCart([FromBody] AddPhoneToCartRequest request)
        {
            if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id))
            {
                var user = _context.Users.Include(u => u.Cart).Where(u => u.Id == id).FirstOrDefault();
                var phone = _context.Phones.Where(p => p.Id == request.PhoneId).FirstOrDefault()!;

                var item = new Item { Phone = phone, Cart = user.Cart, Quantity = request.Quantity };

                _context.Items.Add(item);
                _context.SaveChanges();

                return CreatedAtAction(nameof(AddPhoneToCart), new { message = $"Item added to {user.Username}'s cart." });
            }
            else
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("remove")]
        public IActionResult RemovePhoneFromCart([FromBody] RemovePhoneFromCartRequest request)
        {
            if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id))
            {
                var user = _context.Users.Include(u => u.Cart).Where(u => u.Id == id).FirstOrDefault();

                if (user == null)
                    return Unauthorized(new { message = "The provided token was invalid." });

                var item = _context.Items.FirstOrDefault(i => (i.CartId == user.Cart!.Id) && (i.Id == request.ItemId));

                if (item == null)
                    return NotFound(new { message = "The requested item was not found on the server." });

                _context.Items.Remove(item);
                _context.SaveChanges();

                return NoContent();
            }
            else
            {
                return BadRequest(new { message = "Something went wrong." });
            }

        }
    }
}
