using Application.Areas.Identity.Data;
using Application.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using Application.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Features;


namespace Application.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ItemService _itemService;
        private readonly ReviewService _reviewService;

        public ReviewController(ILogger<ProductsController> logger, ApplicationDbContext context, CartService cartService, SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager, ItemService itemService, ReviewService reviewService )
        {
            _logger = logger;
            _context = context;
            _signInManager=signInManager;
            _userManager=userManager;
            _itemService=itemService;
            _reviewService=reviewService;
        }

    

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        // Display the view to add a review to the concerned item
        public IActionResult AddReviewPage(int itemid){
            Item item= _itemService.GetItemById(itemid);
            return View("AddReview",item);
            
        }

        // Add the review to the database and return a success to the view
        public JsonResult AddReview(int rating, string comment, int itemId){
            string userId = _userManager.GetUserId(User);
            _reviewService.AddReviewToDatabase(rating,comment,itemId,userId);
            return Json(new {success=true});
        }

        // Display the view and pass it the item with its list of reviews and its average rating
        public IActionResult ItemReviews(int itemid){
            Item item= _itemService.GetItemById(itemid);
            List<Review> reviews= _reviewService.GetAllItemReviews(itemid);
            double averageRating = Math.Round(reviews.Average(r => r.rating),1);
            return View("ItemReviews",(item,reviews,averageRating));
        }
    }
}
