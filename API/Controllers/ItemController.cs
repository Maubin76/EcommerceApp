using Application.Areas.Identity.Data;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public JsonResult Create(Item item)
        {
            var ItemInDB = _context.Items.Find(item.id);

            if (ItemInDB == null) _context.Add(item);
            else ItemInDB = item;

            _context.SaveChanges();
            return new JsonResult(Ok(item));
        }

        // Get
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Items.Find(id);
            if (result == null) return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Items.Find(id);
            if (result == null) return new JsonResult(NotFound());

            _context.Items.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        // GetAll
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Items.ToList();

            return new JsonResult(Ok(result));
        }
    }
}
