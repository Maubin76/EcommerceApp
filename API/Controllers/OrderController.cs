using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Areas.Identity.Data;
using Application.Models;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get
        [HttpGet]
        public JsonResult Get(Guid cartId, int itemId)
        {
            var result = _context.CartItems.Find(cartId, itemId);
            if (result == null) return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(Guid cartId, int itemId)
        {
            var result = _context.CartItems.Find(cartId, itemId);
            if (result == null) return new JsonResult(NotFound());

            _context.CartItems.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        // GetAll
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.CartItems.ToList();

            return new JsonResult(Ok(result));
        }

        // Get all orders from 1 user
        [HttpGet()]
        public JsonResult GetAllFrom1User(String id)
        {
            var result = _context.GetAllOdersFromUser(id);

            return new JsonResult(Ok(result));
        }
    }
}
