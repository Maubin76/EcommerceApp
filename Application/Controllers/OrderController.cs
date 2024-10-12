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
            _cartService = cartService; // Inject the CartService
            _signInManager=signInManager;
            _userManager=userManager;
        }

        public IActionResult OrderHistory(){
            
            if (!_signInManager.IsSignedIn(User)){
                return View("OrderHistoryNotLogged");
            }else{
                var cart = _cartService.GetUnactiveCart(_userManager.GetUserId(User));
                if (cart.Count==0){
                    return View("OrderHistoryNotYet");
                }else{
                    return View("OrderHistory",cart);
                }
            }
        }
    }
}