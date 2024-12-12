using Application.Areas.Identity.Data;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public JsonResult Create(Review review)
        {
            var reviewInDB = _context.Reviews.Find(review.id);

            if (reviewInDB == null) _context.Add(review);
            else reviewInDB = review;

            _context.SaveChanges();
            return new JsonResult(Ok(review));
        }

        // Get
        [HttpGet]
        public JsonResult Get(Guid id)
        {
            var result = _context.Reviews.Find(id);
            if (result == null) return new JsonResult(NotFound());
            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(Guid id)
        {
            var result = _context.Reviews.Find(id);
            if (result == null) return new JsonResult(NotFound());

            _context.Reviews.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        // Get all
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Reviews.ToList();

            return new JsonResult(Ok(result));
        }

        // Get all reviews from a user
        [HttpGet()]
        public JsonResult GetReviewsFromUser(String userId)
        {
            var result = _context.GetAllReviewsFromUer(userId);

            return new JsonResult(Ok(result));
        }

        // Get all the review for an item
        [HttpGet()]
        public JsonResult GetreviewsOfItem(int itemId)
        {
            var result = _context.GetAllReviewsOfItem(itemId);

            return new JsonResult(Ok(result));
        }
    }
}