using Newtonsoft.Json;

namespace Application.Models
{
    
    public class CartItem
    {
        public Guid cartId{get;set;} 
        [JsonIgnore]
        public Cart cart {get;set;}
        public int itemId{get;set;} 
        public Item item{get;set;}
         
        public CartItem(){}
        public CartItem(Guid _cartId, Cart _cart, int  _itemId, Item _item)
        {
            cartId = _cartId;
            cart = _cart;
            itemId=_itemId;
            item = _item;
        }
        public CartItem(Guid _cartId, Cart _cart, Item _item)
        {
            cartId = _cartId;
            cart = _cart;
            item = _item;
        }

        public CartItem(Cart _cart, Item _item)
        {
            cart = _cart;
            item = _item;
        }


        
    }
}