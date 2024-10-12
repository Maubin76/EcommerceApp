using Application.Areas.Identity.Data;
using Application.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using Application.Extensions;
using Microsoft.AspNetCore.Identity;


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

    

        
        public async Task<IActionResult> Catalogue()
        {
            var productDisp=await _context.Items.ToListAsync();
            return View(productDisp);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult AddToCart(int id, string name, string description, decimal price, string imageUrl){
            var item = new Item{
                id = id,
                name = name,
                description = description,
                price = price,
                imageUrl = imageUrl};
            _cartService.AddItemToCart(_userManager.GetUserId(User), item);
            return Json(new {success=true});
        }
        public IActionResult Cart(){
            
            if (!_signInManager.IsSignedIn(User)){
                return View("CartNotLogged");
            }else{
                var cart = _cartService.GetActiveCart(_userManager.GetUserId(User));
                if(cart==null){
                    return View("CartLogged");
                }else{
                    if(cart.cartItems.Count==0){
                        return View("CartLogged");
                    }else{
                        return View("CartWithItems",cart);
                    }
                }
            }
        }
        public IActionResult DeleteFromCart(int id, string name, string description, decimal price, string imageUrl){
            var item = new Item{
                id = id,
                name = name,
                description = description,
                price = price,
                imageUrl = imageUrl};
            _cartService.RemoveItemFromCart(_userManager.GetUserId(User), item);
            return Cart();
        }

        public IActionResult Buy(){
            _cartService.BuyCart(_userManager.GetUserId(User));
            return Cart();
        }
    }
}
