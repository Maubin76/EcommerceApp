using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Features;

namespace Application.Models
{
    public class Cart
    {
        

        public Guid id{get;set;} 
        public string userId { get; set; }
        // Attributes isActive is here to know if the cart has been bought or not
        public bool isActive {get; set; }
        public List<CartItem> cartItems { get; set; } = new List<CartItem>();
        public Cart(){}
        
        public Cart(Guid _id, string _userId, List<CartItem> _cartItems)
        {
            id = _id;
            userId = _userId;
            cartItems = _cartItems ?? new List<CartItem>();
            isActive=true;
        }

        // Get the total price of the cart
        public decimal GetPrice(){
            decimal finalPrice=0;
            foreach (CartItem cartItem in this.cartItems){
                finalPrice+=cartItem.item.price*cartItem.quantity;
            }
            return finalPrice;
        }

        
    }
}
