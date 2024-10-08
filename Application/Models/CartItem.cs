namespace Application.Models
{
    public class CartItem
    {
        public Guid cartId{get;set;} 
        public int userId { get; set; }
        public int quantity {get;set;}
        public decimal unitPrice{get;set;}
         

        public CartItem(Guid _cartId, int _userId, decimal _unitPrice, int _quantity)
        {
            cartId = _cartId;
            userId = _userId;
            unitPrice=_unitPrice;
            quantity = _quantity;
        }
    }
}