using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Features;

namespace Application.Models
{
    public class Cart
    {
        

        public Guid id{get;set;} 
        public string userId { get; set; }
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

        public override string ToString()
    {
        //string cartItemsString = string.Join(", ", cartItems.Select(ci => ci.ToString()));
        return $"Cart ID: {id}, User ID: {userId}, Cart Items: test";
    }

        public decimal GetPrice(){
            IEnumerable<Item> items = this.cartItems.Select(ci => ci.item);
            decimal finalPrice=0;
            foreach (Item i in items){
                finalPrice+=i.price;
            }
            return finalPrice;
        }

        
    }
}
