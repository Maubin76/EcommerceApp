using Newtonsoft.Json;

namespace Application.Models
{
    // Class CartItem is doing the link between items and cart
    public class CartItem
    {
        public Guid cartId{get;set;} 
        [JsonIgnore]
        public Cart cart {get;set;}
        public int itemId{get;set;} 
        public Item item{get;set;}
         public int quantity{get;set;}
        public CartItem(){}
        public CartItem(Guid _cartId, Cart _cart, int  _itemId, Item _item,int _quantity)
        {
            cartId = _cartId;
            cart = _cart;
            itemId=_itemId;
            item = _item;
            quantity=_quantity;
        }
        public CartItem(Guid _cartId, Cart _cart, Item _item)
        {
            cartId = _cartId;
            cart = _cart;
            item = _item;
        }

        public CartItem(Cart _cart, Item _item,int _quantity)
        {
            cart = _cart;
            item = _item;
            quantity=_quantity;
        }

        public void addQuantity(){
            this.quantity+=1;
        }

        public void reduceQuantity(){
            this.quantity-=1;
        }

        
    }
}