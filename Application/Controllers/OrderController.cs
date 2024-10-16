using Application.Areas.Identity.Data;
using Application.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using Application.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Application.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ILogger<ProductsController> logger, ApplicationDbContext context, CartService cartService, SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager )
        {
            _logger = logger;
            _context = context;
            _cartService = cartService; 
            _signInManager=signInManager;
            _userManager=userManager;
        }

        // Displays the good view of order history depending on the context
        public IActionResult OrderHistory(){
            
            if (!_signInManager.IsSignedIn(User)){
                return View("OrderHistoryNotLogged"); // Display the good view 
            }else{
                // Get the list of all orders made by the user
                List<Cart> cart = _cartService.GetUnactiveCart(_userManager.GetUserId(User));
                if (cart.Count==0){ // If no order has been made yet
                    return View("OrderHistoryNotYet");
                }else{
                    //Return the view OrderHistory passing the cart object as a model
                    return View("OrderHistory",cart);
                }
            }
        }
    }
}