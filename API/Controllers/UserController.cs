using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Areas.Identity.Data;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create or edit
        [HttpPost]
        public JsonResult CreateEdit(ApplicationUser user)
        {
            var userInDB = _context.Users.Find(user.Id);

            if (userInDB == null) _context.Add(user);
            else userInDB = user;

            _context.SaveChanges();
            return new JsonResult(Ok(user));
        }

        // Get
        [HttpGet]
        public JsonResult Get(String id)
        {
            var result = _context.Users.Find(id);
            if (result == null) return new JsonResult(NotFound());

            return new JsonResult(Ok(result));

        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(String id)
        {
            var result = _context.Users.Find(id);
            if (result == null) return new JsonResult(NotFound());

            _context.Users.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        // GetAll
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Users.ToList();

            return new JsonResult(Ok(result));
        }
    }
}
