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
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ILogger<ProductsController> logger, ApplicationDbContext context, CartService cartService, SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager )
        {
            _logger = logger;
            _context = context;
            _cartService = cartService; // Inject the CartService
            _signInManager=signInManager;
            _userManager=userManager;
        }

    

        
        public async Task<IActionResult> Catalog()
        {
            //Get the list of all the items in the datable
            List<Item> productDisp=await _context.Items.ToListAsync();
            return View(productDisp);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Add an item to cart and send a response to the view to display a pop up
        [HttpPost]
        public JsonResult AddToCart(int id, string name, string description, decimal price, string imageUrl){
            // Create an item object with the attributes sent by the view
            Item item = new Item{
                id = id,
                name = name,
                description = description,
                price = price,
                imageUrl = imageUrl};
            // Add this item to the cart associated to the user
            _cartService.AddItemToCart(_userManager.GetUserId(User), item);
            // Returns a JSON response indicating a success
            return Json(new {success=true});
        }

        // Display the good cart view depending on the context
        public IActionResult Cart(){
            if (!_signInManager.IsSignedIn(User)){ // User not logged in
                return View("CartNotLogged");
            }else{ // User logged in
                // Get the active cart associated to the user
                Cart cart = _cartService.GetActiveCart(_userManager.GetUserId(User));
                if(cart==null){ // User doesn't have a cart 
                    return View("CartLogged"); // Displays view wanted
                }else{ //User has a Cart
                    if(cart.cartItems.Count==0){ // No item in the cart
                        return View("CartLogged");
                    }else{
                        //return the view CartWithItems passing a cart object as a model
                        return View("CartWithItems",cart);
                    }
                }
            }
        }

        // Delete an item from the cart and displays the Cart views updated
        public IActionResult DeleteFromCart(int id, string name, string description, decimal price, string imageUrl){
            // Instantiate an item with the attributes sent by the view
            Item item = new Item{
                id = id,
                name = name,
                description = description,
                price = price,
                imageUrl = imageUrl};
            //Remove item from the cart with this method
            _cartService.RemoveItemFromCart(_userManager.GetUserId(User), item);
            return Cart();
        }

        // Set the order to bought in the database 
        public IActionResult Buy(){
            _cartService.BuyCart(_userManager.GetUserId(User));
            return Cart();
        }
    }
}
